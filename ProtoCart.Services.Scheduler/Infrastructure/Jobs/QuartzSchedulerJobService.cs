using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Business.Repositories.Jobs;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Jobs;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using Quartz;
using Quartz.Spi;

namespace ProtoCart.Services.Scheduler.Infrastructure.Jobs
{
    internal sealed class QuartzSchedulerJobService : InfrastructureUnit, ISchedulerJobService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IJobEntitiesRepository _jobEntitiesRepository;
        private readonly IReadOnlyDictionary<Guid, ISchedulerJobDefinition> _jobIndex;

        public QuartzSchedulerJobService(
            ILogService logService, 
            ISettingsService settingsService,
            ISchedulerFactory schedulerFactory, 
            IJobFactory jobFactory,
            IEnumerable<ISchedulerJobDefinition> jobDefinitions, 
            IJobEntitiesRepository jobEntitiesRepository) 
            : base(logService, settingsService)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobEntitiesRepository = jobEntitiesRepository;
            _jobIndex = jobDefinitions.ToDictionary(definition => definition.Id);
        }

        public void Start()
            => StartAsync(CancellationToken.None).WaitAndUnwrapException();

        public async Task StartAsync(CancellationToken cancellationToken, bool captureContext = false)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken).ConfigureAwait(captureContext);
            Scheduler.JobFactory = _jobFactory;

            foreach (Job job in await _jobEntitiesRepository.ReadAsync(cancellationToken, captureContext))
            {
                _jobIndex.TryGetValue(Guid.Parse(job.JobId), out ISchedulerJobDefinition schedulerJobDefinition);

                if (schedulerJobDefinition is null)
                {
                    LogService.Error?.Write($"Job with id {job.JobId} could not be founded, skipping...");

                    continue;
                }

                IJobDetail jobDetail = CreateJob(schedulerJobDefinition.JobType);
                ITrigger jobTrigger = CreateTrigger(schedulerJobDefinition, job);

                await Scheduler.ScheduleJob(jobDetail, jobTrigger, cancellationToken).ConfigureAwait(captureContext);

            }

            await Scheduler.Start(cancellationToken).ConfigureAwait(captureContext);
        }

        public void Stop()
            => StopAsync(CancellationToken.None).WaitAndUnwrapException();
        
        public async Task StopAsync(CancellationToken cancellationToken, bool captureContext = false)
        {
            if (Scheduler is null)
            {
                return;
            }

            await Scheduler.Shutdown(cancellationToken).ConfigureAwait(false);
        }

        private IScheduler Scheduler { get; set; }

        private static IJobDetail CreateJob(Type jobType)
            => JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();

        private static ITrigger CreateTrigger(ISchedulerJobDefinition schedule, Job job)
            => TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(job.Cron)
                .WithDescription(job.Cron)
                .Build();
    }
}