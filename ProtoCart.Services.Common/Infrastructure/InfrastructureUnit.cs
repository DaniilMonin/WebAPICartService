using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Infrastructure
{
    public abstract class InfrastructureUnit
    {
        protected InfrastructureUnit(ILogService logService, ISettingsService settingsService)
        {
            LogService = logService;
            SettingsService = settingsService;
        }
        
        protected ILogService LogService { get; }
        protected ISettingsService SettingsService { get; }
    }
}