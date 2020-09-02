using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Business.Repositories.Products;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Operations.CartsSweeper
{
    internal sealed  class CartsSweeperOperation : Operation<CartsSweeperRequest>
    {
        private readonly ICartEntitiesRepository _cartEntitiesRepository;
        private readonly IProductEntitiesRepository _productEntitiesRepository;
        private readonly ICartLinksEntitiesRepository _linksEntitiesRepository;

        public CartsSweeperOperation(
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

        protected override async Task DoProcessAsync(CartsSweeperRequest argument, CancellationToken cancellationToken,
            bool captureContext = false)
        {
            if (argument is null)
            {
                return;
            }

            List<Cart> carts = new List<Cart>();
            
            foreach (Cart cart in await _cartEntitiesRepository.ReadAsync(argument.DaysToRemove, cancellationToken, captureContext).ConfigureAwait(captureContext))
            {
                await _linksEntitiesRepository.DeleteByCartIdAsync(cart.Id, cancellationToken, captureContext).ConfigureAwait(captureContext);
                
                await _cartEntitiesRepository.UpdateTimeStampAsync(cart.Id, DateTimeOffset.Now, cancellationToken, captureContext).ConfigureAwait(captureContext);
                
                carts.Add(cart);
            }

            argument.Result = carts;
        }
    }
}