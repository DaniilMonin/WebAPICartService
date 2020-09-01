using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Validation;

namespace ProtoCart.Services.Common.Infrastructure.Operations.Generic
{
    public abstract class RequestOperation<TRequestArgument> : Operation<TRequestArgument>
        where TRequestArgument : Request
    {
        private readonly IValidationPolicy<TRequestArgument> _validationPolicy;

        protected RequestOperation(ILogService logService, ISettingsService settingsService, IValidationPolicy<TRequestArgument> validationPolicy) 
            : base(logService, settingsService)
        {
            _validationPolicy = validationPolicy;
        }
        
        public sealed override async Task ExecuteAsync(TRequestArgument argument, CancellationToken cancellationToken, bool captureContext = false)
        {
            IReadOnlyList<IValidatorResult> result = await ValidationPolicy.ValidateArgumentAsync(argument, cancellationToken, captureContext).ConfigureAwait(captureContext);

            if (result.Count == 0)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
                
                await base.ExecuteAsync(argument, cancellationToken, captureContext);
                
                return;
            }
            
            foreach (IValidatorResult validationResult in result)
            {
                foreach (IValidatorResultMessage message in validationResult.Messages)
                {
                    LogService.Error?.Write($"{message.Property} -> {message.Message}");
                }
            }
            
            ForceStatusToFinish();
        }

        protected IValidationPolicy<TRequestArgument> ValidationPolicy => _validationPolicy;
    }
}