using System;

namespace ProtoCart.Data.Common.Aggregators
{
    public sealed class CartItemAggregator : Entity
    {
        public DateTimeOffset LastUpdateStamp { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Total { get; set; }
        public int TotalBonus { get; set; }
        public int Bonus { get; set; }
        public decimal Price { get; set; }
    }
}