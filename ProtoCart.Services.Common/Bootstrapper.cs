using ProtoCart.Data.Common.Requests;
using ProtoCart.Services.Common.Business.Calculators.GenerateCartReport;
using ProtoCart.Services.Common.Business.Operations.CartsSweeper;
using ProtoCart.Services.Common.Business.Operations.GenerateCartReport;
using ProtoCart.Services.Common.Business.Operations.Hooks;
using ProtoCart.Services.Common.Business.Operations.ModifyCartItems;
using ProtoCart.Services.Common.Business.Reporting;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Registry;

namespace ProtoCart.Services.Common
{
    public static class Bootstrapper
    {
        public static void Init(IServiceRegistry serviceRegistry)
            => serviceRegistry
                .BindAsSingleton<ILogService, LogService>()
                .BindAsSingleton<IReportingService, ReportingService>()
                
                .BindAsTransientToSelf<CartLinksCalculatorProcess>()
                .BindAsFactory<CartLinksCalculatorProcess>()
                
                
                .BindAsTransientToSelf<IOperation<AddHookRequest>, AddHookOperation>()
                .BindAsTransientToSelf<IOperation<CartsSweeperRequest>, CartsSweeperOperation>()
                .BindAsTransientToSelf<IOperation<ModifyCartItemsRequest>, ModifyCartItemsOperation>()
                .BindAsTransientToSelf<IOperation<GenerateCartReportRequest>, GenerateCartReportOperation>()
                
                .BindAsOperationFactory<ModifyCartItemsRequest, ModifyCartItemsOperation>()
                .BindAsOperationFactory<CartsSweeperRequest, CartsSweeperOperation>()
                .BindAsOperationFactory<GenerateCartReportRequest, GenerateCartReportOperation>()
                .BindAsOperationFactory<AddHookRequest, AddHookOperation>();
    }
}