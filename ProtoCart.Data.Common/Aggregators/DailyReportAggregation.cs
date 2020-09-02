using System.Collections.Generic;
using ProtoCart.Data.Common.Entities;

namespace ProtoCart.Data.Common.Aggregators
{
    public sealed class DailyReportAggregation : Aggregator<CartItemAggregator>
    {
        private readonly List<CartItemAggregator> _index = new List<CartItemAggregator>();
        
        public override void Aggregate(CartItemAggregator item)
        {
            _index.Add(item);
        }

        public IReadOnlyCollection<CartItemAggregator> Index => _index;
    }
}