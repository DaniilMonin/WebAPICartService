using Autofac;
using ProtoCart.Services.Common.Infrastructure;
using ProtoCart.Services.Common.Infrastructure.Factories;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Settings;

namespace ProtoCart.Services.Container.Infrastructure.Factories
{
    internal sealed class Factory<TData> : InfrastructureUnit, IFactory<TData>
        where TData : class
    {
        private readonly ILifetimeScope _lifetimeScope;

        public Factory(ILogService logService, ISettingsService settingsService, ILifetimeScope lifetimeScope) : base(logService, settingsService)
        {
            _lifetimeScope = lifetimeScope;
        }

        public TData Create()
            => _lifetimeScope.Resolve<TData>();
    }
}