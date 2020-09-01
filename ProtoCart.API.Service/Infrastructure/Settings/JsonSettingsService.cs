using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.API.Service.Infrastructure.Settings
{
    internal sealed class JsonSettingsService : SettingsService
    {
        public override string Connection => "Data Source=E:/LevelUp/Database/blogging.db";
        public override int HooksRetryCount => 3;
        public override bool IsDebugLogsEnabled => true;
        public override bool IsTraceLogsEnabled => true;
        public override bool IsInfoLogsEnabled => true;
        public override bool IsFatalLogsEnabled => true;
        public override bool IsErrorLogsEnabled => true;
    }
}