using System.Data;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Helpers;
using ProtoCart.Services.Common.Business.Repositories.Reports;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;
using SqlKata.Compilers;

namespace ProtoCart.Data.Database.Business.Repositories.Reports
{
    internal sealed class PeriodCartReportEntitiesRepository : DatabaseEntitiesRepository<PeriodCartReport>, IPeriodCartReportEntitiesRepository
    {
        public PeriodCartReportEntitiesRepository(ILogService logService, ISettingsService settingsService, IDbConnection dbConnection, Compiler compiler) : base(logService, settingsService, dbConnection, compiler)
        {
        }

        protected override string TableName => DatabaseHelper.PeriodReportsTableName;
    }
}