using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Business.Job
{
    public interface IJobService
    {
        void Start();
        Task StartAsync(CancellationToken cancellationToken);
    }
}