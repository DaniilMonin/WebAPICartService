using System.Threading.Tasks;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using Quartz;

namespace ProtoCart.Services.Scheduler.Business.Jobs.DailyReport
{
    internal sealed class DailyReportQuartzSchedulerJob : QuartzSchedulerJob
    {
        public DailyReportQuartzSchedulerJob(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }

        public override Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}