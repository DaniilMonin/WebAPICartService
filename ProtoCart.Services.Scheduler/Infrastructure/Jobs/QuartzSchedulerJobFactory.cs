using ProtoCart.Services.Common.Infrastructure.Jobs;
using Quartz;
using Quartz.Spi;

namespace ProtoCart.Services.Scheduler.Infrastructure.Jobs
{
    internal sealed class QuartzSchedulerJobFactory : IJobFactory
    {
        private readonly ISchedulerJobResolver _schedulerJobResolver;

        public QuartzSchedulerJobFactory(ISchedulerJobResolver schedulerJobResolver)
        {
            _schedulerJobResolver = schedulerJobResolver;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
            => _schedulerJobResolver.Resolve<IQuartzSchedulerJob>(bundle.JobDetail.JobType);

        public void ReturnJob(IJob job)
        {
        }
    }
}