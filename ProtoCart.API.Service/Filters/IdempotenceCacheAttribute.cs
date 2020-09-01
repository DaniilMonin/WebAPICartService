using Microsoft.AspNetCore.Mvc.Filters;

namespace ProtoCart.API.Service.Filters
{
    public sealed class IdempotenceCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}