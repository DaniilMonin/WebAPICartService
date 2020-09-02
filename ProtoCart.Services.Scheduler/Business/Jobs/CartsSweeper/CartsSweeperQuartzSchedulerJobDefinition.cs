using System;
using ProtoCart.Services.Common.Infrastructure.Jobs.Generic;

namespace ProtoCart.Services.Scheduler.Business.Jobs.CartsSweeper
{
    internal sealed class CartsSweeperQuartzSchedulerJobDefinition : SchedulerJobDefinition<CartsSweeperQuartzSchedulerJob>
    {
        public override Guid Id => new Guid("336C632E-67E2-41EF-90E5-13704B4B29AA");
    }
}