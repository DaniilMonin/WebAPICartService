using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Database.Business.Repositories.Carts;
using ProtoCart.Data.Database.Business.Repositories.DocumentsTemplates;
using ProtoCart.Data.Database.Business.Repositories.Hooks;
using ProtoCart.Data.Database.Business.Repositories.Jobs;
using ProtoCart.Data.Database.Business.Repositories.Links;
using ProtoCart.Data.Database.Business.Repositories.Products;
using ProtoCart.Data.Database.Business.Repositories.Reports;
using ProtoCart.Data.Database.Mappers;
using ProtoCart.Services.Common.Business.Repositories.Carts;
using ProtoCart.Services.Common.Business.Repositories.DocumentTemplates;
using ProtoCart.Services.Common.Business.Repositories.Hooks;
using ProtoCart.Services.Common.Business.Repositories.Jobs;
using ProtoCart.Services.Common.Business.Repositories.Links;
using ProtoCart.Services.Common.Business.Repositories.Products;
using ProtoCart.Services.Common.Business.Repositories.Reports;
using ProtoCart.Services.Common.Infrastructure.Registry;
using SqlKata.Compilers;

namespace ProtoCart.Data.Database
{
    public static class Bootstrapper
    {
        private static IServiceRegistry AddMappers(IServiceRegistry serviceRegistry)
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetTypeHandler());

            return serviceRegistry;
        }
        
        public static void Init(IServiceRegistry serviceRegistry)
            =>
                AddMappers(serviceRegistry)
                    .BindUniqueExtendedRepository<Cart, ICartEntitiesRepository, CartEntitiesRepository>()
                    .BindUniqueExtendedRepository<ReportTemplate, IReportTemplateEntitiesRepository, ReportTemplateEntitiesRepository>()
                    .BindUniqueReadOnlyRepository<Job, IJobEntitiesRepository, JobEntitiesRepository>()
                    .BindRepository<CartLink, ICartLinksEntitiesRepository, CartLinksEntitiesRepository>()
                    .BindRepository<Hook, IHookEntitiesRepository, HookEntitiesRepository>()
                    .BindUniqueExtendedRepository<Product, IProductEntitiesRepository, ProductEntitiesRepository>()
                    .BindRepository<PeriodCartReport, IPeriodCartReportEntitiesRepository, PeriodCartReportEntitiesRepository>()
                    .BindAsTransientToSelf<IDbConnection, SqliteConnection>()
                    .BindAsSingleton<Compiler, SqliteCompiler>();
    }
}