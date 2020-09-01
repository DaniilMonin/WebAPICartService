using System;
using ProtoCart.Services.Common.Infrastructure.Jobs.Generic;

namespace ProtoCart.Services.Scheduler.Business.Jobs.Ping
{
    internal sealed class PingQuartzSchedulerJobDefinition : SchedulerJobDefinition<PingQuartzSchedulerJob>
    {
        public override Guid Id => new Guid("1B75FA85-136B-46AF-A48F-579D0565D3AD");
    }
}