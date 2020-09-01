using System;
using ProtoCart.Services.Common.Infrastructure.Jobs.Generic;

namespace ProtoCart.Services.Scheduler.Business.Jobs.DailyReport
{
    internal sealed class DailyReportQuartzSchedulerJobDefinition : SchedulerJobDefinition<DailyReportQuartzSchedulerJob>
    {
        public override Guid Id => new Guid("8CEDC172-90F5-42F4-B567-EFAE91A46E89");
    }
}