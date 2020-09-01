using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtoCart.API.Service.Infrastructure.Controllers;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;

namespace ProtoCart.API.Service.Controllers
{
    public sealed class ReportsController : CartControllerBase
    {
        private readonly IReadOnlyEntitiesRepository<PeriodCartReport> _readOnlyEntitiesRepository;

        public ReportsController(ILogService logService, ISettingsService settingsService, IReadOnlyEntitiesRepository<PeriodCartReport> readOnlyEntitiesRepository) : base(logService, settingsService)
        {
            _readOnlyEntitiesRepository = readOnlyEntitiesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
            => ToJsonResult(await _readOnlyEntitiesRepository.ReadAsync(CancellationToken.None));
    }
}