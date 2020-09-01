using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories.Hooks;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ProtoCart.Data.Database.Business.Repositories.Hooks
{
    public sealed class HookEntitiesRepository : DatabaseEntitiesRepository<Hook>, IHookEntitiesRepository
    {
        public HookEntitiesRepository(ILogService logService, ISettingsService settingsService, IDbConnection dbConnection, Compiler compiler) : base(logService, settingsService, dbConnection, compiler)
        {
        }

        protected override string TableName => DatabaseHelper.HooksTableName;

        public Hook GetByServiceId(string serviceId)
            => GetByServiceIdAsync(serviceId, CancellationToken.None).WaitAndUnwrapException();

        public async Task<Hook> GetByServiceIdAsync(string serviceId, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Db.Query(TableName).Where(DatabaseHelper.ServiceIdHookColumnName, serviceId).FirstOrDefaultAsync<Hook>().ConfigureAwait(captureContext);
        }
    }
}