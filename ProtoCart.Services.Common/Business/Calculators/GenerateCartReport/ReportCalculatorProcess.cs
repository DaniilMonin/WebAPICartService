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
        private readonly ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>> _index = new ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>>();
        
        public ReportCalculatorProcess(ILogService logService, ISettingsService settingsService) : base(logService, settingsService)
        {
        }
        
        public ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>> Calculate(IEnumerable<CartItemAggregator> entities) => CalculateAsync(entities, CancellationToken.None).WaitAndUnwrapException();

        public async Task<ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>>> CalculateAsync(IEnumerable<CartItemAggregator> entities, CancellationToken cancellationToken, bool captureContext = false)
        {
            if (entities is null)
            {
                return _index;
            }

            _index.Clear();
            
            await entities.ForEachAsync(SettingsService.ParallelDegree, async (aggregator, token, captureContext) => await Aggregate(aggregator, token, captureContext), cancellationToken, captureContext).ConfigureAwait(captureContext);

            return _index;
        }

        private async Task<CartItemAggregator> Aggregate(CartItemAggregator itemAggregator, CancellationToken cancellationToken, bool captureContext = false)
        {
            ConcurrentBag<CartItemAggregator> items = _index.SafetyGet(itemAggregator.CartId);

            items.Add(itemAggregator);
            
            await Task.Delay(1, cancellationToken).ConfigureAwait(captureContext);

            return itemAggregator;
        }

        public ConcurrentDictionary<int, ConcurrentBag<CartItemAggregator>> ReportData => _index;
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

