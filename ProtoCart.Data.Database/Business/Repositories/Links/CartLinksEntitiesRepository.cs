using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Aggregators;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Calculators;
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

        public void Calculate(ICalculationProcess<CartItemAggregator> calculationProcess)
            => CalculateAsync(calculationProcess, CancellationToken.None).WaitAndUnwrapException();
        
        public async Task CalculateAsync(ICalculationProcess<CartItemAggregator> calculationProcess, CancellationToken cancellationToken, bool captureContext = false)
        {
            if (calculationProcess is null)
            {
                return;
            }
            
            await Db.Query(DatabaseHelper.DailyReportViewName).ChunkAsync<CartItemAggregator>(SettingsService.ChunkSize, async (chunk, page) =>
            {
                await calculationProcess.CalculateAsync(chunk, cancellationToken);

            }).ConfigureAwait(captureContext);
        }

        public void DeleteByCartId(int cartId)
            => DeleteByCartIdAsync(cartId, CancellationToken.None).WaitAndUnwrapException();

        public async Task DeleteByCartIdAsync(int cartId, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await Db.Query(TableName)
                .Where(DatabaseHelper.CartIdLinkTableColumnName, cartId) 
                .DeleteAsync().ConfigureAwait(captureContext);
        }

        public override async Task UpdateAsync(CartLink entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Db.Query(TableName)
                .Where(DatabaseHelper.CartIdLinkTableColumnName, entity.CartId)
                .Where(DatabaseHelper.ProductIdLinkTableColumnName, entity.ProductId)
                .UpdateAsync(entity).ConfigureAwait(captureContext);
        }

        public override async Task DeleteAsync(CartLink entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await Db.Query(TableName)
                .Where(DatabaseHelper.CartIdLinkTableColumnName, entity.CartId) 
                .Where(DatabaseHelper.ProductIdLinkTableColumnName, entity.ProductId) 
                .DeleteAsync().ConfigureAwait(captureContext);
        }

        protected override string TableName => DatabaseHelper.CartToProductLinksTableName;
        
    }
}