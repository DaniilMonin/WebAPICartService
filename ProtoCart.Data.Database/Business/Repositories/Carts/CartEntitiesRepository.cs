using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;

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

        protected override string TableName => DatabaseHelper.CartTableName;
    }
}