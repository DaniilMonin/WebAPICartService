using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Business.Repositories.Products;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Operations.CleanOldCarts
{
    internal sealed  class CleanOldCartsOperation : Operation<CleanOldCartsRequest>
    {
        private readonly ICartEntitiesRepository _cartEntitiesRepository;
        private readonly IProductEntitiesRepository _productEntitiesRepository;
        private readonly ICartLinksEntitiesRepository _linksEntitiesRepository;

        public CleanOldCartsOperation(
            ILogService logService, 
            ISettingsService settingsService,
            ICartEntitiesRepository cartEntitiesRepository,
            IProductEntitiesRepository productEntitiesRepository,
            ICartLinksEntitiesRepository linksEntitiesRepository) : base(logService, settingsService)
        {
            _cartEntitiesRepository = cartEntitiesRepository;
            _productEntitiesRepository = productEntitiesRepository;
            _linksEntitiesRepository = linksEntitiesRepository;
        }

        protected override async Task DoProcessAsync(CleanOldCartsRequest argument, CancellationToken cancellationToken, bool captureContext = false)
        {
            await Task.Delay(1000, cancellationToken);
        }
    }
}