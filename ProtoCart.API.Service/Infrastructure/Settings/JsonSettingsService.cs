using Microsoft.Extensions.Configuration;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.API.Service.Infrastructure.Settings
{
    internal sealed class JsonSettingsService : SettingsService
    {
        public JsonSettingsService()
        {
            // This is bad solution. Settings should be from Consul or something like that
            
            IConfigurationSection root = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings");

            Connection = root[nameof(Connection)];
            HooksRetryCount = TryParse(root[nameof(HooksRetryCount)], 3);
            ChunkSize = TryParse(root[nameof(ChunkSize)], 250);
            ParallelDegree = TryParse(root[nameof(ParallelDegree)], 10);
            SweeperDays = TryParse(root[nameof(SweeperDays)], 30);
            DailyReportTemplateId = TryParse(root[nameof(DailyReportTemplateId)], 1);
        }
        
        public override string Connection { get; }
        public override int HooksRetryCount { get; }
        public override int ChunkSize { get; }
        public override int DailyReportTemplateId { get; }
        public override int ParallelDegree { get; }
        public override int SweeperDays { get; }
        public override bool IsDebugLogsEnabled => true;
        public override bool IsTraceLogsEnabled => true;
        public override bool IsInfoLogsEnabled => true;
        public override bool IsFatalLogsEnabled => true;
        public override bool IsErrorLogsEnabled => true;

        private int TryParse(string text, int defaultValue)
        {
            int val = defaultValue;
            
            if (string.IsNullOrWhiteSpace(text))
            {
                return val;
            }
            
            try
            {
                int.TryParse(text, out val);
            }
            catch
            {
                // ignored
            }

            return val;
        }
    }
}