using System.Threading.Tasks;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Scheduler.Infrastructure.Jobs;
using Quartz;

namespace ProtoCart.Services.Scheduler.Business.Jobs
{
    [DisallowConcurrentExecution]
    public abstract class QuartzSchedulerJob : InfrastructureUnit, IQuartzSchedulerJob
    {
        protected QuartzSchedulerJob(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }
        
        public abstract Task Execute(IJobExecutionContext context);
        
    }
}