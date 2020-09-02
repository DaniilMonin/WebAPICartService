using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtoCart.API.Service.Filters;
using ProtoCart.API.Service.Infrastructure.Controllers;
using ProtoCart.Data.Common.Operations;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.API.Service.Controllers
{
    public sealed class CartController : CartControllerBase
    {
        private readonly ICartLinksEntitiesRepository _linksEntitiesRepository;
        private readonly IFactory<IOperation<ModifyCartItemsRequest>> _modifyCartItemsOperationFactory;
        private const int OnlyOneItem = 1;

        public CartController(ILogService logService, ISettingsService settingsService, ICartLinksEntitiesRepository linksEntitiesRepository, IFactory<IOperation<ModifyCartItemsRequest>> modifyCartItemsOperationFactory) : base(logService, settingsService)
        {
            _linksEntitiesRepository = linksEntitiesRepository;
            _modifyCartItemsOperationFactory = modifyCartItemsOperationFactory;
        }

        [HttpGet]
        public async Task<IActionResult> List(int cartId)
            => ToJsonResult(await _linksEntitiesRepository.GetLinksByCartIdAsync(cartId, CancellationToken.None));
        
        [HttpPut, IdempotenceCache]
        public async Task<IActionResult> Add(int cartId, int productId)
            => ToJsonResult(await Add(cartId, productId, OnlyOneItem));
        
        [HttpPut("Bulk"), IdempotenceCache]
        public async Task<IActionResult> Add(int cartId, int productId, int total)
            => ToJsonResult(await ModifyCartAsync(cartId, productId, total, CartItemOperation.Increment));

        [HttpPut, IdempotenceCache]
        public async Task<IActionResult> Remove(int cartId, int productId)
            => ToJsonResult(await Remove(cartId, productId, OnlyOneItem));

        [HttpPut("Bulk"), IdempotenceCache]
        public async Task<IActionResult> Remove(int cartId, int productId, int total)
            => ToJsonResult(await ModifyCartAsync(cartId, productId, total, CartItemOperation.Decrement));
        
        
        private async Task<IActionResult> ModifyCartAsync(int cartId, int productId, int total, CartItemOperation itemOperation)
        {
            IOperation<ModifyCartItemsRequest> operation = _modifyCartItemsOperationFactory.Create();
            
            await operation.ExecuteAsync(new ModifyCartItemsRequest { CartId = cartId, ProductId = productId, Total = total, CartItemOperation = itemOperation }, CancellationToken.None);
            
            return ToJsonResult(operation);
        }
    }
}