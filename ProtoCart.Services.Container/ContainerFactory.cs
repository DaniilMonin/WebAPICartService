using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Container.Infrastructure.Registry;

namespace ProtoCart.Services.Container
{
    public sealed class ContainerFactory<TSettingsProvider> : IServiceProviderFactory<ContainerBuilder>
        where TSettingsProvider : class, ISettingsService
    {
        private readonly ContainerBuilder _containerBuilder;

        public ContainerFactory()
        {
            AutofacServiceRegistry serviceRegistry = new AutofacServiceRegistry();

            //infra
            serviceRegistry.BindSettings<TSettingsProvider>();
            Log.Bootstrapper.Init(serviceRegistry);
            Common.Bootstrapper.Init(serviceRegistry);
            
            //scheduler
            Scheduler.Bootstrapper.Init(serviceRegistry);

            //validators
            Data.Validation.Bootstrapper.Init(serviceRegistry);

            //templates
            Templates.Bootstrapper.Init(serviceRegistry);

            //repository
            Data.Database.Bootstrapper.Init(serviceRegistry);


            _containerBuilder = serviceRegistry.ContainerBuilder;
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            _containerBuilder.Populate(services);

            return _containerBuilder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            if (containerBuilder == null)
            {
                throw new ArgumentNullException(nameof(containerBuilder));
            }

            return new AutofacServiceProvider(containerBuilder.Build());
        }
    }
}