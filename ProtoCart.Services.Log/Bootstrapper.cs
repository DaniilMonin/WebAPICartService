using ProtoCart.Services.Common.Infrastructure.Registry;
using ProtoCart.Services.Log.Infrastructure.Logger;

namespace ProtoCart.Services.Log
{
    public static class Bootstrapper
    {
        public static void Init(IServiceRegistry serviceRegistry)
            => serviceRegistry.BindLogWriter<ConsoleLogWriter>();
    }
}