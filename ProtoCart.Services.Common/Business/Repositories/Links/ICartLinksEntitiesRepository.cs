using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Repositories.Generic;

namespace ProtoCart.Services.Common.Business.Repositories.Links
{
    public interface ICartLinksEntitiesRepository : IEntitiesRepository<CartLink>
    {
        IEnumerable<CartLink> GetLinksByCartId(int cartId);
        Task<IEnumerable<CartLink>> GetLinksByCartIdAsync(int cartId, CancellationToken cancellationToken, bool captureContext = false);
    }
}