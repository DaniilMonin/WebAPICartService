using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Operations;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Validation;

namespace ProtoCart.Services.Common.Business.Operations.ModifyCartItems
{
    internal sealed  class ModifyCartItemsOperation : RequestOperation<ModifyCartItemsRequest>
    {
        private readonly ICartEntitiesRepository _cartEntitiesRepository;
        private readonly ICartLinksEntitiesRepository _linksEntitiesRepository;

        public ModifyCartItemsOperation(
            ILogService logService, 
            ISettingsService settingsService, 
            IValidationPolicy<ModifyCartItemsRequest> validationPolicy,
            ICartEntitiesRepository cartEntitiesRepository,
            ICartLinksEntitiesRepository linksEntitiesRepository) 
            : base(logService, settingsService, validationPolicy)
        {
            _cartEntitiesRepository = cartEntitiesRepository;
            _linksEntitiesRepository = linksEntitiesRepository;
        }

        protected override async Task DoProcessAsync(ModifyCartItemsRequest argument, CancellationToken cancellationToken, bool captureContext = false)
        {
            try
            {
                CartLink link = (await _linksEntitiesRepository
                        .ReadAsync(cancellationToken, captureContext)
                        .ConfigureAwait(captureContext))
                    .FirstOrDefault(x => x.CartId == argument.CartId && x.ProductId == argument.ProductId);

                switch (argument.CartItemOperation)
                {
                    case CartItemOperation.Increment when link is null:
                    {
                        await CreateNewLink(argument, cancellationToken, captureContext).ConfigureAwait(captureContext);
                    }
                        break;
                    case CartItemOperation.Increment:
                    {
                        await UpdateOrDeleteLink(link, true, cancellationToken, captureContext)
                            .ConfigureAwait(captureContext);
                    }
                        break;
                    case CartItemOperation.Decrement when link is null:
                    {
                        LogService.Error?.Write($"Cart with id '{argument.CartId}' doesn't contains product with id '{argument.ProductId}, exiting...'");
                    }
                        break;
                    case CartItemOperation.Decrement:
                    {
                        await UpdateOrDeleteLink(link, false, cancellationToken, captureContext)
                            .ConfigureAwait(captureContext);
                    }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception exception)
            {
                LogService.Error?.Write(exception.Message);
            }
        }

        private async Task CreateNewLink(ModifyCartItemsRequest argument, CancellationToken cancellationToken, bool captureContext = false)
        {
            CartLink cartLink = new CartLink
            {
                CartId = argument.CartId,
                ProductId = argument.ProductId,
                Total = argument.Total
            };

            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
                        
            await _linksEntitiesRepository.CreateAsync(cartLink, cancellationToken, captureContext).ConfigureAwait(captureContext);
            await _cartEntitiesRepository.UpdateTimeStampAsync(cartLink.CartId, DateTimeOffset.UtcNow, cancellationToken, captureContext).ConfigureAwait(captureContext);
        }

        private async Task UpdateOrDeleteLink(CartLink cartLink, bool increment, CancellationToken cancellationToken, bool captureContext = false)
        {
            if (cartLink is null)
            {
                throw new ArgumentNullException(nameof(cartLink));
            }
            
            if (increment)
            {
                cartLink.Total++;
            }
            else
            {
                cartLink.Total--;
            }
            
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            
            if (cartLink.Total == 0)
            {
                await _linksEntitiesRepository.DeleteAsync(cartLink, cancellationToken, captureContext).ConfigureAwait(captureContext);
                
                await _cartEntitiesRepository.UpdateTimeStampAsync(cartLink.CartId, DateTimeOffset.UtcNow, cancellationToken, captureContext).ConfigureAwait(captureContext);

                return;
            }
                        
            await _linksEntitiesRepository.UpdateAsync(cartLink, cancellationToken, captureContext).ConfigureAwait(captureContext);
            
            await _cartEntitiesRepository.UpdateTimeStampAsync(cartLink.CartId, DateTimeOffset.UtcNow, cancellationToken, captureContext).ConfigureAwait(captureContext);
        }
    }
}