namespace ProtoCart.Data.Common.Entities
{
    public sealed class CartLink : Entity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Total { get; set; }
    }
}