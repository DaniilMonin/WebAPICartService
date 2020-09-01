namespace ProtoCart.Data.Common.Requests
{
    public sealed class AddHookRequest : Request
    {
        public string ServiceId { get; set; }
        public string ServiceUri { get; set; }
    }
}