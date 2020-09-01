using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Repositories.Generic
{
    public abstract class EntitiesRepository<TEntity> : InfrastructureUnit, IEntitiesRepository<TEntity>
        where TEntity : Entity
    {
        protected EntitiesRepository(
            ILogService logService, 
            ISettingsService settingsService) 
            : base(logService, settingsService)
        {
        }


        public void Create(TEntity entity) =>
            CreateAsync(entity, CancellationToken.None).WaitAndUnwrapException();
        
        public virtual Task CreateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Read() =>
            ReadAsync(CancellationToken.None).WaitAndUnwrapException();
        
        public virtual Task<IEnumerable<TEntity>> ReadAsync(CancellationToken cancellationToken, bool captureContext = false)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TEntity entity) =>
            UpdateAsync(entity, CancellationToken.None).WaitAndUnwrapException();

        
        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TEntity entity) =>
            DeleteAsync(entity, CancellationToken.None).WaitAndUnwrapException();
        
        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            throw new System.NotImplementedException();
        }
        
    }
}