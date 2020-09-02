using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ProtoCart.Data.Database.Business.Repositories.Carts
{
    internal sealed class CartEntitiesRepository : UniqueDatabaseExtendedEntitiesRepository<Cart>, ICartEntitiesRepository
    {
        public CartEntitiesRepository(
            ILogService logService, 
            ISettingsService settingsService, 
            IDbConnection dbConnection,
            Compiler compiler) 
            : base(logService, settingsService, dbConnection, compiler)
        {
        }

        public override Task DeleteAsync(Cart entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            throw new NotSupportedException();
        }

        public void UpdateTimeStamp(int cartId, DateTimeOffset offset)
            => UpdateTimeStampAsync(cartId, offset, CancellationToken.None).WaitAndUnwrapException();

        public async Task UpdateTimeStampAsync(int cartId, DateTimeOffset offset, CancellationToken cancellationToken,
            bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Db.Query(TableName).Where(DatabaseHelper.IdColumnName, cartId).UpdateAsync(
                new Dictionary<string, object>
                {
                    {DatabaseHelper.LastUpdateStampColumnName, offset}
                }).ConfigureAwait(captureContext);
        }

        public IEnumerable<Cart> Read(int days)
            => ReadAsync(days, CancellationToken.None).WaitAndUnwrapException();

        public async Task<IEnumerable<Cart>> ReadAsync(int days, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return 
                await Db.Query(TableName)
                .WhereRaw($"date({DatabaseHelper.LastUpdateStampColumnName}) < date('now','-{days} days')")
                .GetAsync<Cart>()
                .ConfigureAwait(captureContext);
        }
        
        protected override string TableName => DatabaseHelper.CartTableName;
        
    }
}