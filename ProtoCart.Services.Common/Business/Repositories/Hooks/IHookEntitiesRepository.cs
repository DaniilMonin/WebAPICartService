using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Repositories.Generic;

namespace ProtoCart.Services.Common.Business.Repositories.Hooks
{
    public interface IHookEntitiesRepository : IEntitiesRepository<Hook>
    {
        Hook GetByServiceId(string serviceId);
        Task<Hook> GetByServiceIdAsync(string serviceId, CancellationToken cancellationToken, bool captureContext = false);
    }
}