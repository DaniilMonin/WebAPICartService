using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ProtoCart.Data.Database.Business.Repositories
{
    public abstract class DatabaseEntitiesRepository<TEntity> : EntitiesRepository<TEntity>
        where TEntity : Entity
    {
        private readonly QueryFactory _db;
        
        protected DatabaseEntitiesRepository(
            ILogService logService, 
            ISettingsService settingsService,
            IDbConnection dbConnection,
            Compiler compiler) 
            : base(logService, settingsService)
        {
            dbConnection.ConnectionString = ConnectionString;
            
            _db = new QueryFactory(dbConnection, compiler);
        }

        public override async Task<IEnumerable<TEntity>> ReadAsync(CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _db.Query(TableName).GetAsync<TEntity>().ConfigureAwait(captureContext);
        }

        public override async Task CreateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await _db.Query(TableName).InsertAsync(entity).ConfigureAwait(captureContext);
        }

        public override async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await _db.Query(TableName).UpdateAsync(entity).ConfigureAwait(captureContext);
        }

        public override async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await _db.Query(TableName).UpdateAsync(entity).ConfigureAwait(captureContext);
        }

        protected abstract string TableName { get; }
        
        protected QueryFactory Db => _db;
        
        private string ConnectionString => SettingsService.Connection;
    }
}