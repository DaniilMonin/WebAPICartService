using System;
using System.Threading.Tasks;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using Quartz;

namespace ProtoCart.Services.Scheduler.Business.Jobs.Ping
{
    internal sealed class PingQuartzSchedulerJob : QuartzSchedulerJob
    {
        public PingQuartzSchedulerJob(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            LogService.Info?.Write("Ping");
            
            /*throw new Exception("ololol");*/
            
            await Task.Delay(1);
        }
    }
}