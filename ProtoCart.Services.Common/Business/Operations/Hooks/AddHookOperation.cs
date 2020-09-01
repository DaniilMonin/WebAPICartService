using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Repositories.Hooks;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Validation;

namespace ProtoCart.Services.Common.Business.Operations.Hooks
{
    internal sealed class AddHookOperation : RequestOperation<AddHookRequest>
    {
        private readonly IHookEntitiesRepository _hookEntitiesRepository;

        public AddHookOperation(ILogService logService, ISettingsService settingsService,
            IValidationPolicy<AddHookRequest> validationPolicy, IHookEntitiesRepository hookEntitiesRepository) : base(
            logService, settingsService, validationPolicy)
        {
            _hookEntitiesRepository = hookEntitiesRepository;
        }

        protected override async Task DoProcessAsync(AddHookRequest argument, CancellationToken cancellationToken,
            bool captureContext = false)
            => await _hookEntitiesRepository.CreateAsync(
                new Hook() {ServiceId = argument.ServiceId, ServiceUri = argument.ServiceUri}, cancellationToken,
                captureContext);
    }
}