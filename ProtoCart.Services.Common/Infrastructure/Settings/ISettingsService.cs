namespace ProtoCart.Services.Common.Infrastructure.Settings
{
    public interface ISettingsService
    {
        string Connection { get; }
        int HooksRetryCount { get; }
        int ChunkSize { get; }
        int DailyReportTemplateId { get; }
        int ParallelDegree { get; }
        int SweeperDays { get; }
        bool IsDebugLogsEnabled { get; }
        bool IsTraceLogsEnabled { get; }
        bool IsInfoLogsEnabled { get; }
        bool IsFatalLogsEnabled { get; }
        bool IsErrorLogsEnabled { get; }
    }
}