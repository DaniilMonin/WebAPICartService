namespace ProtoCart.Data.Common.Entities
{
    public sealed class Hook : Entity
    {
        public string ServiceId { get; set; }
        public string ServiceUri { get; set; }
    }
}