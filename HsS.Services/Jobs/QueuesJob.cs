using HsS.Data.Repositories;
using HsS.Ifs.Chains;
using HsS.Ifs.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HsS.Services.Jobs
{
    public class QueuesJob : IJob
    {
        private static object _lock = new object();
        private readonly IServiceProvider serviceProvider;
        private readonly IHubContext<EventsHub> hubContext;
        private readonly ILogger<QueuesJob> logger;

        public QueuesJob(IServiceProvider serviceProvider, IHubContext<EventsHub> hubContext, ILogger<QueuesJob> logger)
        {
            this.serviceProvider = serviceProvider;
            this.hubContext = hubContext;
            this.logger = logger;

            this.logger.LogInformation("Queues job initiated");
        }

        public Task Execute(IJobExecutionContext context)
        {
            lock (_lock)
            {
                var unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
                var repository = unitOfWork.GetRepository<IQueueRepository>();
                var queues = repository.List(q => !q.Processed && q.CreatedDateTime < DateTime.Now.AddSeconds(10), q => q.OrderBy(a => a.CreatedDateTime));
                logger.LogInformation($"{queues.Count} items found to process");
                foreach (var queue in queues)
                {
                    var requestObject = JsonConvert.DeserializeObject(queue.Request, Type.GetType(queue.TypeDefinition));
                    ChainServicesExecuter.Execute(requestObject, null, queue);
                    queue.Processed = true;
                    queue.ProcessDateTime = DateTime.Now;

                    hubContext.Clients.Client(EventsHub.Connections[queue.HubId][0]).SendAsync("eventRaised", "yout order has done", "success");
                }

                unitOfWork.SaveChanges();


                return Task.CompletedTask;
            }
        }
    }
}
