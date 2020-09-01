namespace ProtoCart.Services.Common.Infrastructure.Logger
{
    public interface ILogService
    {
        ILogPipe Debug { get; }
        ILogPipe Info { get; }
        ILogPipe Error { get; }
        ILogPipe Fatal { get; }
        ILogPipe Trace { get; }
    }
}