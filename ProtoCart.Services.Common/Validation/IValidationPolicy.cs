using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Validation
{
    public interface IValidationPolicy<TArgument>
        where TArgument : class
    {
        IReadOnlyList<IValidatorResult> ValidateArgument(TArgument entity);
        Task<IReadOnlyList<IValidatorResult>> ValidateArgumentAsync(TArgument entity, CancellationToken cancellationToken, bool captureContext = false);
    }
}