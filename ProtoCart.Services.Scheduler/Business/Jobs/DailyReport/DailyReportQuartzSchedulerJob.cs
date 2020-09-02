using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using Quartz;

namespace ProtoCart.Services.Scheduler.Business.Jobs.DailyReport
{
    internal sealed class DailyReportQuartzSchedulerJob : QuartzSchedulerJob
    {
        private readonly IFactory<IOperation<GenerateCartReportRequest>> _factory;

        public DailyReportQuartzSchedulerJob(
            ILogService logService, 
            ISettingsService settingsService,
            IFactory<IOperation<GenerateCartReportRequest>> factory) 
            : base(logService, settingsService)
        {
            _factory = factory;
        }

        public override async Task Execute(IJobExecutionContext context)
            => await _factory
                .Create()
                .ExecuteAsync(new GenerateCartReportRequest {ReportTemplateId = 1}, CancellationToken.None);
    }
}