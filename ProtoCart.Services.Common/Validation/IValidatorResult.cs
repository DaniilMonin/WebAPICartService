using System.Collections.Generic;

namespace ProtoCart.Services.Common.Validation
{
    public interface IValidatorResult
    {
        IReadOnlyList<IValidatorResultMessage> Messages { get; }
    }
}