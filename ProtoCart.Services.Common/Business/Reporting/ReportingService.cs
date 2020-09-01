using ProtoCart.Services.Common.Business.Repositories.Reports;
using ProtoCart.Services.Common.Business.Templating;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Reporting
{
    public abstract class ReportingService : InfrastructureUnit, IReportingService
    {
        private readonly ITemplatingService _templatingService;
        private readonly IPeriodCartReportEntitiesRepository _periodCartReportEntitiesRepository;

        protected ReportingService(ILogService logService, ISettingsService settingsService, ITemplatingService templatingService, IPeriodCartReportEntitiesRepository periodCartReportEntitiesRepository) : base(logService, settingsService)
        {
            _templatingService = templatingService;
            _periodCartReportEntitiesRepository = periodCartReportEntitiesRepository;
        }
    }
}