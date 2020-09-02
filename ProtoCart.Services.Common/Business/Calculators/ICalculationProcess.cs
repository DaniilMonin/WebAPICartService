using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Common.Aggregators;

namespace ProtoCart.Services.Common.Business.Calculators
{
    public interface ICalculationProcess<TEntity>
        where TEntity : Entity
    {
        ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>> Calculate(IEnumerable<TEntity> entities);
        Task<ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>>> CalculateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool captureContext = false);
    }
}