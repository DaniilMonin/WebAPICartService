using System;

namespace ProtoCart.Services.Common.Infrastructure.Jobs.Generic
{
    public abstract class SchedulerJobDefinition<TJob> : ISchedulerJobDefinition<TJob>
        where TJob : class, ISchedulerJob
    {
        public abstract Guid Id { get; }
        
        public Type JobType => typeof(TJob);
    }
}