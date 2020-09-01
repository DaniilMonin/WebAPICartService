using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProtoCart.API.Service.Infrastructure.Settings;
using ProtoCart.Services.Container;

namespace ProtoCart.API.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new ContainerFactory<JsonSettingsService>())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}