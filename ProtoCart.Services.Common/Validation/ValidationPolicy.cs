using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Validation
{
    internal sealed class ValidationPolicy<TArgument> : InfrastructureUnit, IValidationPolicy<TArgument>
        where TArgument : class
    {
        private readonly IEnumerable<IValidatorPolicyItem<TArgument>> _validatorItems;

        public ValidationPolicy(ILogService logService, ISettingsService settingsService, IEnumerable<IValidatorPolicyItem<TArgument>> validatorItems) : base(logService, settingsService)
        {
            _validatorItems = validatorItems;
        }

        public IReadOnlyList<IValidatorResult> ValidateArgument(TArgument entity) => ValidateArgumentAsync(entity, CancellationToken.None).WaitAndUnwrapException();

        public async Task<IReadOnlyList<IValidatorResult>> ValidateArgumentAsync(TArgument entity, CancellationToken cancellationToken, bool captureContext = false)
        {
            if (entity is null)
            {
                return null;
            }

            List<IValidatorResult> results = new List<IValidatorResult>();
            
            foreach (IValidatorPolicyItem<TArgument> policyItem in _validatorItems)
            {
                IValidatorResult result = await policyItem.ValidateArgumentAsync(entity, cancellationToken, captureContext).ConfigureAwait(captureContext);

                if (result.Messages.Count == 0)
                {
                    continue;
                }
                
                results.Add(result);
            }

            return results;
        }
    }
}