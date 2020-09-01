using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace ProtoCart.Data.Database.Business.Repositories.Links
{
    internal sealed class CartLinksEntitiesRepository : DatabaseEntitiesRepository<CartLink>, ICartLinksEntitiesRepository
    {
        public CartLinksEntitiesRepository(ILogService logService, ISettingsService settingsService, IDbConnection dbConnection, Compiler compiler) : base(logService, settingsService, dbConnection, compiler)
        {
        }

        public IEnumerable<CartLink> GetLinksByCartId(int cartId)
            => GetLinksByCartIdAsync(cartId, CancellationToken.None).WaitAndUnwrapException();

        public async Task<IEnumerable<CartLink>> GetLinksByCartIdAsync(int cartId, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Db.Query(TableName).Where(DatabaseHelper.CartIdLinkTableColumnName, cartId).GetAsync<CartLink>().ConfigureAwait(captureContext);
        }

        protected override string TableName => DatabaseHelper.CartToProductLinksTableName;
        
    }
}