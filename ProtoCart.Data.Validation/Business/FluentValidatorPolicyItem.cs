using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Validation;

namespace ProtoCart.Data.Validation.Business
{
    public abstract class FluentValidatorPolicyItem<TArgument> : AbstractValidator<TArgument>, IValidatorPolicyItem<TArgument>
        where TArgument : class
    {
        public IValidatorResult ValidateArgument(TArgument entity) =>
            ValidateArgumentAsync(entity, CancellationToken.None).WaitAndUnwrapException();

        public async Task<IValidatorResult> ValidateArgumentAsync(
            TArgument entity, 
            CancellationToken cancellationToken,
            bool captureContext = false)
        {
            ValidationResult result = await ValidateAsync(entity, cancellationToken).ConfigureAwait(captureContext);

            if (result is null)
            {
                return null;
            }

            List<IValidatorResultMessage> validationResults = new List<IValidatorResultMessage>();

            foreach (ValidationFailure validationFailureItem in result.Errors)
            {
                validationResults.Add(new ValidatorResultMessage(validationFailureItem.PropertyName, validationFailureItem.ErrorMessage));
            }

            return new ValidatorResult(validationResults);
        }

        private sealed class ValidatorResult : IValidatorResult
        {
            public ValidatorResult(IReadOnlyList<IValidatorResultMessage> messages)
            {
                Messages = messages;
            }

            public IReadOnlyList<IValidatorResultMessage> Messages { get; }
        }

        private sealed class ValidatorResultMessage : IValidatorResultMessage
        {
            public ValidatorResultMessage(string property, string message)
            {
                Property = property;
                Message = message;
            }

            public string Property { get; }
            public string Message { get; }
        }
    }
}