using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;

namespace ProtoCart.Services.Common.Business.Repositories.Carts
{
    public interface ICartEntitiesRepository : IUniqueEntitiesExtendedRepository<Cart>
    {
        void UpdateTimeStamp(int cartId, DateTimeOffset offset);
        Task UpdateTimeStampAsync(int cartId, DateTimeOffset offset, CancellationToken cancellationToken, bool captureContext = false);
        IEnumerable<Cart> Read(int days);
        Task<IEnumerable<Cart>> ReadAsync(int days, CancellationToken cancellationToken, bool captureContext = false);
    }
}