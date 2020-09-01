using System;
using Autofac;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Jobs;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Registry;
using ProtoCart.Services.Container.Infrastructure.Factories;

namespace ProtoCart.Services.Container.Infrastructure.Registry
{
    internal sealed class AutofacServiceRegistry : ServiceRegistry
    {
        private readonly ContainerBuilder _containerBuilder = new ContainerBuilder();

        public AutofacServiceRegistry()
        {
            BindAsSingleton<ISchedulerJobResolver, AutofacSchedulerJobResolver>();
        }
        
        public ContainerBuilder ContainerBuilder => _containerBuilder;
        
        public override IServiceRegistry BindAsSingleton<TAbstraction, TImplementation>()
        {
            _containerBuilder.RegisterType<TImplementation>().As<TAbstraction>().SingleInstance();
            
            return this;
        }

        public override IServiceRegistry BindAsOperationFactory<TArgument, TImplementation>()
        {
            _containerBuilder.RegisterType<Factory<TImplementation>>().As<IFactory<IOperation<TArgument>>>().SingleInstance();
            
            return this;
        }

        protected override IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2>()
        {
            _containerBuilder.RegisterType<TImplementation2>().As<TImplementation1>().As<TAbstraction>().SingleInstance();

            return this;
        }

        protected override IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2, TImplementation3>()
        {
            _containerBuilder.RegisterType<TImplementation3>().As<TImplementation1>().As<TImplementation2>().As<TAbstraction>().SingleInstance();

            return this;
        }
        
        protected override IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2, TImplementation3, TImplementation4>()
        {
            _containerBuilder.RegisterType<TImplementation4>().As<TImplementation1>().As<TImplementation2>().As<TImplementation3>().As<TAbstraction>().SingleInstance();

            return this;
        }
        
        protected override IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2, TImplementation3, TImplementation4, TImplementation5>()
        {
            _containerBuilder.RegisterType<TImplementation5>().As<TImplementation4>().As<TImplementation1>().As<TImplementation2>().As<TImplementation3>().As<TAbstraction>().SingleInstance();

            return this;
        }

        public override IServiceRegistry BindAsTransientToSelf<TAbstraction, TImplementation>()
        {
            _containerBuilder.RegisterType<TImplementation>().As<TImplementation>().As<TAbstraction>().ExternallyOwned();

            return this;
        }

        public override IServiceRegistry BindAsTransientToSelf<TImplementation>()
            where TImplementation : class
        {
            _containerBuilder.RegisterType<TImplementation>().As<TImplementation>().ExternallyOwned();

            return this;
        }

        private sealed class AutofacSchedulerJobResolver : ISchedulerJobResolver
        {
            private readonly IComponentContext  _container;

            public AutofacSchedulerJobResolver(IComponentContext  container)
            {
                _container = container;
            }

            public TSchedulerJob Resolve<TSchedulerJob>(Type serviceType)
                where TSchedulerJob : class, ISchedulerJob
                => _container.Resolve(serviceType) as TSchedulerJob;
        }
    }
}