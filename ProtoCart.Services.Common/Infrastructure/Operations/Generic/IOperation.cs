using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Infrastructure.Operations.Generic
{
    public interface IOperation<TArgument> : IOperation
        where TArgument : class
    {
        Task ExecuteAsync(TArgument argument, CancellationToken cancellationToken, bool captureContext = false);
    }
}