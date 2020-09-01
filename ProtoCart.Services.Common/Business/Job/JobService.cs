using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Services.Common.Business.Repositories.Jobs;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Job
{
    public abstract class JobService : InfrastructureUnit, IJobService
    {
        protected JobService(ILogService logService, ISettingsService settingsService, IJobEntitiesRepository jobEntitiesRepository) : base(logService, settingsService)
        {
            JobEntitiesRepository = jobEntitiesRepository;
        }

        public void Start() => StartAsync(CancellationToken.None).WaitAndUnwrapException();
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (JobEntitiesRepository is null)
            {
                return;
            }

            foreach (Data.Common.Entities.Job job in await JobEntitiesRepository.ReadAsync(cancellationToken))
            {
                Start(job);
            }
        }
        
        protected abstract void Start(Data.Common.Entities.Job job);
        
        private IJobEntitiesRepository JobEntitiesRepository { get; }
    }
}