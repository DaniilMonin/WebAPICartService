using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Infrastructure.Jobs
{
    public interface ISchedulerJobService
    {
        void Start();
        Task StartAsync(CancellationToken cancellationToken, bool captureContext = false);
        void Stop();
        Task StopAsync(CancellationToken cancellationToken, bool captureContext = false);
    }
}