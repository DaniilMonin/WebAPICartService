using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Services.Common.Repositories.Generic;

namespace ProtoCart.Services.Common.Business.Repositories
{
    public interface IUniqueEntitiesExtendedRepository<TEntity> : IUniqueEntitiesRepository<TEntity>
        where TEntity : UniqueEntity
    {
        TEntity GetEntityById(int id);
        Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken, bool captureContext = false);
    }
}