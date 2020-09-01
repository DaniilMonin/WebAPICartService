using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;

namespace ProtoCart.Services.Common.Repositories.Generic
{
    public interface IEntitiesRepository<TEntity> : IReadOnlyEntitiesRepository<TEntity>
        where TEntity : Entity
    {
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false);
    }
}