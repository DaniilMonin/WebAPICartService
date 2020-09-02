using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Aggregators;
using ProtoCart.Services.Common.Extensions.Tasks;
using ProtoCart.Services.Common.Extensions.Tasks.Synchronous;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Common.Business.Calculators.GenerateCartReport
{
    internal sealed class ReportCalculatorProcess : InfrastructureUnit, ICalculationProcess<CartItemAggregator>
    {
        private readonly ConcurrentDictionary<int, ConcurrentDictionary<int, object>> _index = new ConcurrentDictionary<int, ConcurrentDictionary<int, object>>();
        
        public ReportCalculatorProcess(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }
        
        public void Calculate(IEnumerable<CartItemAggregator> entities) => CalculateAsync(entities, CancellationToken.None).WaitAndUnwrapException();

        public async Task CalculateAsync(IEnumerable<CartItemAggregator> entities, CancellationToken cancellationToken, bool captureContext = false)
        {
            if (entities is null)
            {
                return;
            }

            await entities.ForEachAsync(SettingsService.ParallelDegree, async (aggregator, token, captureContext) => await Aggregate(aggregator, token, captureContext), cancellationToken, captureContext).ConfigureAwait(captureContext);
        }

        private async Task<CartItemAggregator> Aggregate(CartItemAggregator itemAggregator, CancellationToken cancellationToken, bool captureContext = false)
        {

            ConcurrentDictionary<int, object> productIndex = _index.SafetyGet(itemAggregator.CartId);

            object product = productIndex.SafetyGet(itemAggregator.ProductId);
            
            
            await Task.Delay(1, cancellationToken).ConfigureAwait(false);

            return itemAggregator;
        }
        
        public object ReportData { get; private set; }
    }

    internal static class ConcurrentDictionaryHelper
    {
        public static TItem SafetyGet<TItem>(this ConcurrentDictionary<int, TItem> index, int id)
            where TItem : class, new()
        {
            index.TryGetValue(id, out TItem item);

            if (item is null)
            {
                item = new TItem();

                index.TryAdd(id, item);
            }

            return item;
        }
    }
    
}

