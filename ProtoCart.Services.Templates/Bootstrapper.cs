using ProtoCart.Services.Common.Infrastructure.Registry;
using ProtoCart.Services.Templates.Business.Templating;

namespace ProtoCart.Services.Templates
{
    public static class Bootstrapper
    {
        public static void Init(IServiceRegistry serviceRegistry)
            => serviceRegistry.BindTemplatingService<ScribanTemplatingService>();
    }
}