using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;

namespace ProtoCart.Services.Common.Repositories.Generic
{
    public interface IReadOnlyEntitiesRepository<TEntity>
        where TEntity : Entity
    {
        IEnumerable<TEntity> Read();
        Task<IEnumerable<TEntity>> ReadAsync(CancellationToken cancellationToken, bool captureContext = false);
    }
}