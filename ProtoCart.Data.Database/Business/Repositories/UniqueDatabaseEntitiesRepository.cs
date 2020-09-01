using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ProtoCart.Data.Database.Business.Repositories
{
    public abstract class UniqueDatabaseEntitiesRepository<TEntity> : DatabaseEntitiesRepository<TEntity>, IUniqueEntitiesRepository<TEntity>
        where TEntity : UniqueEntity
    {
        protected UniqueDatabaseEntitiesRepository(
            ILogService logService, 
            ISettingsService settingsService,
            IDbConnection dbConnection,
            Compiler compiler) 
            : base(logService, settingsService, dbConnection, compiler)
        {
        }

        public override async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await Db.Query(TableName).Where(DatabaseHelper.IdColumnName, entity.Id).UpdateAsync(entity).ConfigureAwait(captureContext);
        }

        public override async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await Db.Query(TableName).Where(DatabaseHelper.IdColumnName, entity.Id).DeleteAsync().ConfigureAwait(captureContext);
        }
    }
}