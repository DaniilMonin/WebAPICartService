using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Services.Common.Business.Repositories.Reports;
using ProtoCart.Services.Common.Business.Templating;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Reporting
{
    internal sealed class ReportingService : InfrastructureUnit, IReportingService
    {
        private readonly ITemplatingService _templatingService;
        private readonly IPeriodCartReportEntitiesRepository _periodCartReportEntitiesRepository;

        public ReportingService(ILogService logService, ISettingsService settingsService, ITemplatingService templatingService, IPeriodCartReportEntitiesRepository periodCartReportEntitiesRepository) : base(logService, settingsService)
        {
            _templatingService = templatingService;
            _periodCartReportEntitiesRepository = periodCartReportEntitiesRepository;
        }

        public void Generate<TDataReport>(int templateId, TDataReport dataReport)
        {
            throw new System.NotImplementedException();
        }

        public Task GenerateAsync<TDataReport>(int templateId, TDataReport dataReport, CancellationToken cancellationToken,
            bool captureContext = false)
        {
            throw new System.NotImplementedException();
        }
    }
}