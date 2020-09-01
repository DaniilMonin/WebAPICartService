using System;

namespace ProtoCart.Services.Common.Infrastructure.Jobs
{
    public interface ISchedulerJobDefinition
    {
        Guid Id { get; }
        
        Type JobType { get; }
    }
}