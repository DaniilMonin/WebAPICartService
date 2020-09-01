using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ProtoCart.Data.Database.Business.Repositories
{
    public abstract class UniqueDatabaseExtendedEntitiesRepository<TEntity> : UniqueDatabaseEntitiesRepository<TEntity>, IUniqueEntitiesExtendedRepository<TEntity>
        where TEntity : UniqueEntity
    {
        protected UniqueDatabaseExtendedEntitiesRepository(
            ILogService logService, 
            ISettingsService settingsService, 
            IDbConnection dbConnection, 
            Compiler compiler) 
            : base(logService, settingsService, dbConnection, compiler)
        {
        }

        public TEntity GetEntityById(int id)
            => GetEntityByIdAsync(id, CancellationToken.None).WaitAndUnwrapException();

        public async Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            return await Db.Query(TableName).Where(DatabaseHelper.IdColumnName, id).FirstOrDefaultAsync<TEntity>().ConfigureAwait(captureContext);
        }
    }
}