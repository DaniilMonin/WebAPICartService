using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtoCart.API.Service.Infrastructure.Controllers;
using ProtoCart.Services.Common.Business.Repositories.Jobs;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.API.Service.Controllers
{
    public sealed class JobsController : CartControllerBase
    {
        private readonly IJobEntitiesRepository _jobEntitiesRepository;

        public JobsController(ILogService logService, ISettingsService settingsService, IJobEntitiesRepository jobEntitiesRepository) : base(logService, settingsService)
        {
            _jobEntitiesRepository = jobEntitiesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
            => ToJsonResult(await _jobEntitiesRepository.ReadAsync(CancellationToken.None));
    }
}