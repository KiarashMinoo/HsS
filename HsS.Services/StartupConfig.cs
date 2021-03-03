using HsS.Ifs.Schedulers;
using HsS.Services.Commands.Handlers;
using HsS.Services.Jobs;
using HsS.Services.Queries.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace HsS.Services
{
    public static class StartupConfig
    {
        public static void ServicesConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Schedulers
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<QueuesJob>();

            services.AddSingleton(new JobSchedule(jobType: typeof(QueuesJob), cronExpression: "0/5 * * * * ?")); // run every 5 seconds

            services.AddHostedService<QuartzHostedService>();

            services.AddScoped<IQueueRequestCommandHandler, QueueRequestCommandHandler>();

            services.AddScoped<IListSharesQueryHandler, ListSharesQueryHandler>();
            services.AddScoped<IQueueFollowUpQueryHandler, QueueFollowUpQueryHandler>();
        }
    }
}
