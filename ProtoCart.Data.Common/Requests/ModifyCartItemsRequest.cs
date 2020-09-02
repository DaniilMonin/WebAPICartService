using ProtoCart.Data.Common.Operations;

namespace ProtoCart.Data.Common.Requests
{
    public sealed class ModifyCartItemsRequest : Request
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Total { get; set; }
        public CartItemOperation CartItemOperation { get; set; }
    }
}