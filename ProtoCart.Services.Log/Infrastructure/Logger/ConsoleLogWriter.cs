using System;
using Microsoft.Extensions.Logging;
using ProtoCart.Services.Common.Infrastructure.Logger;

namespace ProtoCart.Services.Log.Infrastructure.Logger
{
    internal sealed class ConsoleLogWriter : ILogWriter
    {
        public void Write(LogLevel level, string message)
            => Console.WriteLine($"{level} {DateTime.UtcNow} {message}");
    }
}