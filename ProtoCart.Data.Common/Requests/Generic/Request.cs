namespace ProtoCart.Data.Common.Requests.Generic
{
    public abstract class Request<TResult> : Request
        where TResult : class
    {
        public TResult Result { get; set; }
    }
}