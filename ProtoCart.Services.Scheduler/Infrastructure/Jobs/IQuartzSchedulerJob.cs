using ProtoCart.Services.Common.Infrastructure.Jobs;
using Quartz;

namespace ProtoCart.Services.Scheduler.Infrastructure.Jobs
{
    public interface IQuartzSchedulerJob : ISchedulerJob, IJob
    {
        
    }
}