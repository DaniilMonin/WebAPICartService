using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Business.Repositories.DocumentTemplates;
using ProtoCart.Services.Common.Business.Templating;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using Scriban;

namespace ProtoCart.Services.Templates.Business.Templating
{
    internal sealed class ScribanTemplatingService : TemplatingService
    {
        private readonly IReportTemplateEntitiesRepository _reportTemplateEntitiesRepository;

        public ScribanTemplatingService(ILogService logService, ISettingsService settingsService, IReportTemplateEntitiesRepository reportTemplateEntitiesRepository) : base(logService, settingsService)
        {
            _reportTemplateEntitiesRepository = reportTemplateEntitiesRepository;
        }

        public override async Task<string> RenderAsync<TReportData>(
            int templateId, 
            TReportData data,
            CancellationToken cancellationToken, 
            bool captureContext = false)
            where TReportData : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            ReportTemplate reportTemplate = await _reportTemplateEntitiesRepository.GetEntityByIdAsync(templateId, cancellationToken, captureContext);

            return await Template.Parse(reportTemplate.Body).RenderAsync(data).ConfigureAwait(captureContext);
        }
    }
}