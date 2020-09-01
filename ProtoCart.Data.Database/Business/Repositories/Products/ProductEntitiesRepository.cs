using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories.Products;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;

namespace ProtoCart.Data.Database.Business.Repositories.Products
{
    internal sealed class ProductEntitiesRepository : UniqueDatabaseExtendedEntitiesRepository<Product>, IProductEntitiesRepository
    {
        public ProductEntitiesRepository(ILogService logService, ISettingsService settingsService, IDbConnection dbConnection, Compiler compiler) : base(logService, settingsService, dbConnection, compiler)
        {
        }

        public override Task DeleteAsync(Product entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            throw new NotSupportedException();
        }

        protected override string TableName => DatabaseHelper.ProductTableName;
    }
}