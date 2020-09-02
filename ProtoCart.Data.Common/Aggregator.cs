namespace ProtoCart.Data.Common
{
    public abstract class Aggregator<TItem>
        where TItem : class
    {
        public abstract void Aggregate(TItem item);
    }
}