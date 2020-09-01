namespace ProtoCart.Services.Common.Infrastructure.Logger
{
    public interface ILogPipe
    {
        void Write(string message);
    }
}