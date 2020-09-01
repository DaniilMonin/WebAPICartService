using ProtoCart.Data.Common.Requests;
using ProtoCart.Data.Validation.Business.Validators;
using ProtoCart.Services.Common.Infrastructure.Registry;

namespace ProtoCart.Data.Validation
{
    public static class Bootstrapper
    {
        public static void Init(IServiceRegistry serviceRegistry)
            => serviceRegistry
                .BindValidators<ModifyCartItemsRequest, ModifyCartItemsRequestValidatorItem>()
                .BindValidators<AddHookRequest, AddHookRequestValidatorItem>();
    }
}