using Microsoft.AspNetCore.Mvc;
using ProtoCart.API.Service.Data.Responses;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.API.Service.Infrastructure.Controllers
{
    [ApiController, Route("api/v1/[controller]/[action]")]
    public abstract class CartControllerBase : ControllerBase
    {
        protected CartControllerBase(ILogService logService, ISettingsService settingsService)
        {
            LogService = logService;
            SettingsService = settingsService;
        }
        
        protected ILogService LogService { get; }
        protected ISettingsService SettingsService { get; }

        protected JsonResult ToJsonResult(IOperation operation)
            => ToJsonResult<OperationStatus>(operation.Status);
        
        protected JsonResult ToJsonResult<TData>(TData data)
            => new JsonResult(new ResultResponse<TData>(data));
    }
}