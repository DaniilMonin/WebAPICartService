using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Aggregators;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Extensions.Tasks;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Calculators.GenerateCartReport
{
    internal sealed class CartLinksCalculatorProcess : InfrastructureUnit, ICalculationProcess<CartLink>
    {
        public CartLinksCalculatorProcess(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }
        
        public void Calculate(IEnumerable<CartLink> entities) => CalculateAsync(entities, CancellationToken.None).WaitAndUnwrapException();

        public async Task CalculateAsync(IEnumerable<CartLink> entities, CancellationToken cancellationToken, bool captureContext = false)
        {
            if (entities is null)
            {
                return;
            }

            DailyReportAggregation[] aggregations = await entities.ForEachAsync<DailyReportAggregation, CartLink>(SettingsService.ParallelDegree, null, cancellationToken, captureContext).ConfigureAwait(captureContext);
        }
        
        public object ReportData { get; private set; }
    }
}