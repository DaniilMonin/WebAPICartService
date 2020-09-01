using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ProtoCart.Data.Common;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Business.Repositories.Products;

namespace ProtoCart.Data.Validation.Business.Validators
{
    internal sealed class ModifyCartItemsRequestValidatorItem : FluentValidatorPolicyItem<ModifyCartItemsRequest>
    {
        public ModifyCartItemsRequestValidatorItem(
            ICartEntitiesRepository cartEntitiesRepository,
            IProductEntitiesRepository productEntitiesRepository)
        {
            
            RuleFor(x => x.CartId).MustAsync(async (id, cancellation) =>
            {
                return await IsItemExist(cartEntitiesRepository, id, cancellation);
                
            }).WithMessage("Cart with id {PropertyValue} could not be found");
            
            RuleFor(x => x.ProductId).MustAsync(async (id, cancellation) =>
            {
                return await IsItemExist(productEntitiesRepository, id, cancellation);
                
            }).WithMessage("Product with id {PropertyValue} could not be found");
            
        }

        private async Task<bool> IsItemExist<TEntity>(IUniqueEntitiesExtendedRepository<TEntity> repository, int id, CancellationToken cancellationToken)
            where TEntity : UniqueEntity
        {
            if (repository is null)
            {
                return false;
            }

            TEntity uniqueEntity = await repository.GetEntityByIdAsync(id, cancellationToken);

            if (uniqueEntity is null)
            {
                return false;
            }
            
            return true;
        }
    }
}