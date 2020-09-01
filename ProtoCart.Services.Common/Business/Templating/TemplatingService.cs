using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Templating
{
    public abstract class TemplatingService : InfrastructureUnit, ITemplatingService
    {
        protected TemplatingService(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }
        
        public string Render<TReportData>(int templateId, TReportData data) where TReportData : class => RenderAsync(templateId, data, CancellationToken.None).WaitAndUnwrapException();

        public abstract Task<string> RenderAsync<TReportData>(int templateId, TReportData data, CancellationToken cancellationToken, bool captureContext = false)
            where TReportData : class ;
    }
}