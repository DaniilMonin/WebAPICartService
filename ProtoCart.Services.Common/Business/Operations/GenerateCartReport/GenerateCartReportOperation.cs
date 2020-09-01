using System;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Business.Repositories.Hooks;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Business.Repositories.Products;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Operations.GenerateCartReport
{
    internal sealed class GenerateCartReportOperation : Operation<GenerateCartReportRequest>
    {
        private readonly ICartEntitiesRepository _cartEntitiesRepository;
        private readonly IProductEntitiesRepository _productEntitiesRepository;
        private readonly ICartLinksEntitiesRepository _linksEntitiesRepository;
        private readonly IHookEntitiesRepository _hookEntitiesRepository;

        public GenerateCartReportOperation(
            ILogService logService, 
            ISettingsService settingsService, 
            ICartEntitiesRepository cartEntitiesRepository,
            IProductEntitiesRepository productEntitiesRepository,
            ICartLinksEntitiesRepository linksEntitiesRepository,
            IHookEntitiesRepository hookEntitiesRepository) 
            : base(logService, settingsService)
        {
            _cartEntitiesRepository = cartEntitiesRepository;
            _productEntitiesRepository = productEntitiesRepository;
            _linksEntitiesRepository = linksEntitiesRepository;
            _hookEntitiesRepository = hookEntitiesRepository;
        }

        protected override async Task DoProcessAsync(GenerateCartReportRequest argument, CancellationToken cancellationToken, bool captureContext = false)
        {
            int totalCarts = 0;
            int totalPrice = 0;

            foreach (Cart cart in await _cartEntitiesRepository.ReadAsync(cancellationToken, captureContext).ConfigureAwait(captureContext))
            {
                totalCarts++;
                
                
            }
        }
    }
}