using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;

namespace ProtoCart.Services.Common.Business.Calculators
{
    public interface ICalculationProcess<TEntity>
        where TEntity : Entity
    {
        void Calculate(IEnumerable<TEntity> entities);
        Task CalculateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool captureContext = false);
    }
}