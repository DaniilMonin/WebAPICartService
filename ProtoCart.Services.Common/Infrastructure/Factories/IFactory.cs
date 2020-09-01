namespace ProtoCart.Services.Common.Infrastructure.Factories
{
    public interface IFactory<out TData>
        where TData : class
    {
        TData Create();
    }
}