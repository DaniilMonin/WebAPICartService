namespace ProtoCart.Services.Common.Infrastructure.Settings
{
    public abstract class SettingsService : ISettingsService
    {
        public abstract string Connection { get; }
        public abstract int HooksRetryCount { get; }
        public abstract int ChunkSize { get; }
        public abstract int ParallelDegree { get; }
        public abstract int SweeperDays { get; }
        public abstract bool IsDebugLogsEnabled { get; }
        public abstract bool IsTraceLogsEnabled { get; }
        public abstract bool IsInfoLogsEnabled { get; }
        public abstract bool IsFatalLogsEnabled { get; }
        public abstract bool IsErrorLogsEnabled { get; }
    }
}