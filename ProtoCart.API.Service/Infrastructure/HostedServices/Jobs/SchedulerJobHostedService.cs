using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ProtoCart.Services.Common.Infrastructure.Jobs;

namespace ProtoCart.API.Service.Infrastructure.HostedServices.Jobs
{
    internal sealed class SchedulerJobHostedService : IHostedService
    {
        private readonly ISchedulerJobService _schedulerJobService;

        public SchedulerJobHostedService(ISchedulerJobService schedulerJobService)
        {
            _schedulerJobService = schedulerJobService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
            => await _schedulerJobService.StartAsync(cancellationToken);

        public async Task StopAsync(CancellationToken cancellationToken)
            => await _schedulerJobService.StopAsync(cancellationToken);
    }
}