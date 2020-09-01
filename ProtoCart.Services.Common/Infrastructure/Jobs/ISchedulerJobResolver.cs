using System;

namespace ProtoCart.Services.Common.Infrastructure.Jobs
{
    public interface ISchedulerJobResolver
    {
        TSchedulerJob Resolve<TSchedulerJob>(Type serviceType)
            where TSchedulerJob : class, ISchedulerJob;
    }
}