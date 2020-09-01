using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Calculators.GenerateCartReport;
using ProtoCart.Services.Common.Business.Reporting;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Operations.GenerateCartReport
{
    internal sealed class GenerateCartReportOperation : Operation<GenerateCartReportRequest>
    {
        private readonly IReportingService _reportingService;
        private readonly ICartLinksEntitiesRepository _linksEntitiesRepository;
        private readonly IFactory<CartLinksCalculatorProcess> _linksCalculator;

        public GenerateCartReportOperation(
            ILogService logService, 
            ISettingsService settingsService, 
            IReportingService reportingService,
            ICartLinksEntitiesRepository linksEntitiesRepository,
            IFactory<CartLinksCalculatorProcess> linksCalculator) 
            : base(logService, settingsService)
        {
            _reportingService = reportingService;
            _linksEntitiesRepository = linksEntitiesRepository;
            _linksCalculator = linksCalculator;
        }

        protected override async Task DoProcessAsync(GenerateCartReportRequest argument,
            CancellationToken cancellationToken, bool captureContext = false)
        {
            CartLinksCalculatorProcess linksCalculatorProcess = _linksCalculator.Create();

            await _linksEntitiesRepository.CalculateAsync(linksCalculatorProcess, cancellationToken, captureContext)
                .ConfigureAwait(captureContext);

            await _reportingService
                .GenerateAsync(argument.ReportTemplateId, linksCalculatorProcess.ReportData, cancellationToken,
                    captureContext).ConfigureAwait(captureContext);
        }
    }
}