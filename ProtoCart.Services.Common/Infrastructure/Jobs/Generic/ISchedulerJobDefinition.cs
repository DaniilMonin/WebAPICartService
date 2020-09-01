namespace ProtoCart.Services.Common.Infrastructure.Jobs.Generic
{
    public interface ISchedulerJobDefinition<TJob> : ISchedulerJobDefinition
        where TJob : class, ISchedulerJob
    {

    }
}