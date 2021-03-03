using HsS.Data.Repositories;
using HsS.Ifs.Cqrs;
using HsS.Ifs.Data;
using HsS.Models.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace HsS.Services.Commands.Handlers
{
    public interface IQueueRequestCommandHandler : ICommandHandler<QueueRequestCommand>
    {
    }

    internal class QueueRequestCommandHandler : CommandHandler<QueueRequestCommand>, IQueueRequestCommandHandler
    {
        private readonly IQueueRepository repository;
        private readonly ILogger<QueueRequestCommandHandler> logger;

        public QueueRequestCommandHandler(IUnitOfWork unitOfWork, ILogger<QueueRequestCommandHandler> logger) : base(unitOfWork)
        {
            repository = unitOfWork.GetRepository<IQueueRepository>();
            this.logger = logger;
        }

        protected override void HandleCommand(QueueRequestCommand command)
        {
            OnCommandExecuting($"Initiating new queue");

            logger.LogInformation("Initiating new queue");
            var queue = new Queue
            {
                Processed = false,
                TransactionId = long.Parse(DateTime.Now.ToString("yyMMddHHmmssfff")),
                Request = JsonConvert.SerializeObject(command.Request),
                TypeDefinition = command.Request.GetType().AssemblyQualifiedName,
                HubId = command.HubId,
                CreatedDateTime = DateTime.Now
            };
            logger.LogInformation($"Queue has initiated by transaction identity value ({queue.TransactionId})");

            repository.Add(queue);
            UnitOfWork.SaveChanges();
            logger.LogInformation($"Queue has persisted into database with id ({queue.Id})");

            OnCommandExecuting($"Queue has persisted into database with id ({queue.Id})");
        }
    }
}
