using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Infrastructure.Logger
{
    internal sealed class LogService : ILogService
    {
        public LogService(IEnumerable<ILogWriter> writers, ISettingsService settingsService)
        {
            Debug = TryCreateLogPipe(LogLevel.Debug, settingsService.IsDebugLogsEnabled, writers);
            Info = TryCreateLogPipe(LogLevel.Information, settingsService.IsInfoLogsEnabled, writers);
            Error = TryCreateLogPipe(LogLevel.Error, settingsService.IsErrorLogsEnabled, writers);
            Fatal = TryCreateLogPipe(LogLevel.Critical, settingsService.IsFatalLogsEnabled, writers);
            Trace = TryCreateLogPipe(LogLevel.Trace, settingsService.IsTraceLogsEnabled, writers);
        }
        
        public ILogPipe Debug { get; }
        public ILogPipe Info { get; }
        public ILogPipe Error { get; }
        public ILogPipe Fatal { get; }
        public ILogPipe Trace { get; }

        private ILogPipe TryCreateLogPipe(LogLevel level, bool create, IEnumerable<ILogWriter> writers)
            => create ? new LogPipe(level, writers) : null;
        
        private sealed class LogPipe: ILogPipe
        {
            private readonly IEnumerable<ILogWriter> _logWriters;
            private readonly LogLevel _level;

            public LogPipe(LogLevel level, IEnumerable<ILogWriter> logWriters)
            {
                _level = level;
                _logWriters = logWriters;
            }
            
            public void Write(string message)
            {
                foreach (ILogWriter logWriter in _logWriters)
                {
                    logWriter.Write(_level, message);
                }
            }
        }
    }
}