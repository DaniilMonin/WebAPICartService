using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtoCart.API.Service.Infrastructure.Controllers;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;

namespace ProtoCart.API.Service.Controllers
{
    public sealed class HooksController : CartControllerBase
    {
        private readonly IReadOnlyEntitiesRepository<Hook> _hookEntitiesRepository;
        private readonly IFactory<IOperation<AddHookRequest>> _hookOperationFactory;

        public HooksController(ILogService logService, ISettingsService settingsService, IReadOnlyEntitiesRepository<Hook> hookEntitiesRepository, IFactory<IOperation<AddHookRequest>> hookOperationFactory) : base(logService, settingsService)
        {
            _hookEntitiesRepository = hookEntitiesRepository;
            _hookOperationFactory = hookOperationFactory;
        }

        [HttpGet]
        public async Task<IActionResult> List()
            => ToJsonResult(await _hookEntitiesRepository.ReadAsync(CancellationToken.None));

        [HttpPost]
        public async Task<IActionResult> Add(AddHookRequest hook)
        {
            IOperation<AddHookRequest> operation = _hookOperationFactory.Create();
            
            await operation.ExecuteAsync(hook, CancellationToken.None);
            
            return ToJsonResult(operation);
        }
    }
}