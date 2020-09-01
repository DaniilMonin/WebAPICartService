using System;
using FluentValidation;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Hooks;

namespace ProtoCart.Data.Validation.Business.Validators
{
    internal sealed class AddHookRequestValidatorItem : FluentValidatorPolicyItem<AddHookRequest>
    {
        public AddHookRequestValidatorItem(IHookEntitiesRepository hookEntitiesRepository)
        {
            RuleFor(x => x.ServiceId).MustAsync(async (id, cancellation) =>
            {
                return await hookEntitiesRepository.GetByServiceIdAsync(id, cancellation) is null;
                
            }).WithMessage("Service with id {PropertyValue} already created");
            
            RuleFor(x => x.ServiceUri).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Service uri - {PropertyValue} should be valid uri path");
            
        }
    }
}