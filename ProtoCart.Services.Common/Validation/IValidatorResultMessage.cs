namespace ProtoCart.Services.Common.Validation
{
    public interface IValidatorResultMessage
    {
        public string Property { get; }
        public string Message { get; }
    }
}