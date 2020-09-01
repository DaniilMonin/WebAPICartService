using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Validation
{
    public interface IValidatorPolicyItem<TArgument>
        where TArgument : class
    {
        IValidatorResult ValidateArgument(TArgument entity);
        Task<IValidatorResult> ValidateArgumentAsync(TArgument entity, CancellationToken cancellationToken, bool captureContext = false);
    }
}