namespace ProtoCart.API.Service.Data.Responses
{
    public sealed class ResultResponse<TResult>
    {
        private readonly TResult _result;

        public ResultResponse(TResult result)
        {
            _result = result;
        }

        public TResult Result => _result;
    }
}