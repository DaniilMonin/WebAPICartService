using Microsoft.Extensions.Logging;

namespace ProtoCart.Services.Common.Infrastructure.Logger
{
    public interface ILogWriter
    {
        void Write(LogLevel level, string message);
    }
}