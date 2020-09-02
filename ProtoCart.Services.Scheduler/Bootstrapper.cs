using ProtoCart.Services.Common.Infrastructure.Jobs;
using ProtoCart.Services.Common.Infrastructure.Registry;
using ProtoCart.Services.Scheduler.Business.Jobs.CartsSweeper;
using ProtoCart.Services.Scheduler.Business.Jobs.DailyReport;
using ProtoCart.Services.Scheduler.Business.Jobs.Ping;
using ProtoCart.Services.Scheduler.Infrastructure.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace ProtoCart.Services.Scheduler
{
    public static class Bootstrapper
    {
        public static void Init(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .BindAsTransientToSelf<ISchedulerJobDefinition, PingQuartzSchedulerJobDefinition>()
                .BindAsTransientToSelf<IQuartzSchedulerJob, PingQuartzSchedulerJob>();
            
            serviceRegistry
                .BindAsTransientToSelf<ISchedulerJobDefinition, DailyReportQuartzSchedulerJobDefinition>()
                .BindAsTransientToSelf<IQuartzSchedulerJob, DailyReportQuartzSchedulerJob>();
            
            serviceRegistry
                .BindAsTransientToSelf<ISchedulerJobDefinition, CartsSweeperQuartzSchedulerJobDefinition>()
                .BindAsTransientToSelf<IQuartzSchedulerJob, CartsSweeperQuartzSchedulerJob>();
            
            serviceRegistry
                .BindAsSingleton<IJobFactory, QuartzSchedulerJobFactory>()
                .BindAsSingleton<ISchedulerFactory, StdSchedulerFactory>()
                .BindSchedulerJobService<QuartzSchedulerJobService>();
            
        }
    }
}