using ProtoCart.Data.Common;
using ProtoCart.Services.Common.Business.Repositories;
using ProtoCart.Services.Common.Business.Templating;
using ProtoCart.Services.Common.Infrastructure.Jobs;
using ProtoCart.Services.Common.Infrastructure.Logger;
using ProtoCart.Services.Common.Infrastructure.Operations;
using ProtoCart.Services.Common.Infrastructure.Operations.Generic;
using ProtoCart.Services.Common.Infrastructure.Settings;
using ProtoCart.Services.Common.Repositories.Generic;
using ProtoCart.Services.Common.Validation;

namespace ProtoCart.Services.Common.Infrastructure.Registry
{
    public abstract class ServiceRegistry : IServiceRegistry
    {
        public IServiceRegistry BindSettings<TSettingsProvider>()
            where TSettingsProvider : class, ISettingsService
            => BindAsSingleton<ISettingsService, TSettingsProvider>();

        public IServiceRegistry BindLogWriter<TLogWriter>() where TLogWriter : class, ILogWriter
            => BindAsSingleton<ILogWriter, TLogWriter>();

        public IServiceRegistry BindTemplatingService<TTemplatingService>()
            where TTemplatingService : class, ITemplatingService
            => BindAsSingleton<ITemplatingService, TTemplatingService>();

        public IServiceRegistry BindSchedulerJobService<TSchedulerJobService>()
            where TSchedulerJobService : class, ISchedulerJobService
            => BindAsSingleton<ISchedulerJobService, TSchedulerJobService>();
        
        public IServiceRegistry BindValidators<TArgument, TValidatorItem>()
            where TArgument : class
            where TValidatorItem : class, IValidatorPolicyItem<TArgument>
        {
            BindAsSingleton<IValidatorPolicyItem<TArgument>, TValidatorItem>();
            
            BindAsSingleton<IValidationPolicy<TArgument>, ValidationPolicy<TArgument>>();

            return this;
        }

        public IServiceRegistry BindValidators<TArgument, TValidatorItem1, TValidatorItem2>()
            where TArgument : class
            where TValidatorItem1 : class, IValidatorPolicyItem<TArgument>
            where TValidatorItem2 : class, IValidatorPolicyItem<TArgument>
        {
            BindAsSingleton<IValidatorPolicyItem<TArgument>, TValidatorItem1>();
            BindAsSingleton<IValidatorPolicyItem<TArgument>, TValidatorItem2>();

            BindAsSingleton<IValidationPolicy<TArgument>, ValidationPolicy<TArgument>>();
            
            return this;
        }

        public IServiceRegistry BindValidators<TArgument, TValidatorItem1, TValidatorItem2, TValidatorItem3>()
            where TArgument : class
            where TValidatorItem1 : class, IValidatorPolicyItem<TArgument>
            where TValidatorItem2 : class, IValidatorPolicyItem<TArgument>
            where TValidatorItem3 : class, IValidatorPolicyItem<TArgument>
        {
            BindAsSingleton<IValidatorPolicyItem<TArgument>, TValidatorItem1>();
            BindAsSingleton<IValidatorPolicyItem<TArgument>, TValidatorItem2>();
            BindAsSingleton<IValidatorPolicyItem<TArgument>, TValidatorItem3>();

            BindAsSingleton<IValidationPolicy<TArgument>, ValidationPolicy<TArgument>>();
            
            return this;
        }

        public IServiceRegistry BindRepository<TEntity, TRepositoryAbstraction, TRepository>()
            where TEntity : Entity
            where TRepositoryAbstraction : class, IEntitiesRepository<TEntity>
            where TRepository : class, IEntitiesRepository<TEntity>, TRepositoryAbstraction
            => BindAsSingleton<IReadOnlyEntitiesRepository<TEntity>, IEntitiesRepository<TEntity>, TRepositoryAbstraction, TRepository>();

        public IServiceRegistry BindUniqueRepository<TEntity, TRepositoryAbstraction, TUniqueEntityRepository>()
            where TEntity : UniqueEntity
            where TRepositoryAbstraction : class, IUniqueEntitiesRepository<TEntity>
            where TUniqueEntityRepository : class, IUniqueEntitiesRepository<TEntity>, TRepositoryAbstraction
            => BindAsSingleton<IReadOnlyEntitiesRepository<TEntity>, IEntitiesRepository<TEntity>, IUniqueEntitiesRepository<TEntity>, TRepositoryAbstraction, TUniqueEntityRepository>();
        
        public IServiceRegistry BindUniqueReadOnlyRepository<TEntity, TRepositoryAbstraction, TUniqueEntityRepository>()
            where TEntity : UniqueEntity
            where TRepositoryAbstraction : class, IReadOnlyEntitiesRepository<TEntity>
            where TUniqueEntityRepository : class, IUniqueEntitiesRepository<TEntity>, TRepositoryAbstraction
            => BindAsSingleton<IReadOnlyEntitiesRepository<TEntity>, TRepositoryAbstraction, TUniqueEntityRepository>();

        public IServiceRegistry BindUniqueExtendedRepository<TEntity, TRepositoryAbstraction, TUniqueEntityRepository>()
            where TEntity : UniqueEntity
            where TRepositoryAbstraction : class, IUniqueEntitiesExtendedRepository<TEntity>
            where TUniqueEntityRepository : class, IUniqueEntitiesExtendedRepository<TEntity>, TRepositoryAbstraction
            => BindAsSingleton<IReadOnlyEntitiesRepository<TEntity>, IEntitiesRepository<TEntity>, IUniqueEntitiesRepository<TEntity>, IUniqueEntitiesExtendedRepository<TEntity>, TRepositoryAbstraction, TUniqueEntityRepository>();

        public abstract IServiceRegistry BindAsSingleton<TAbstraction, TImplementation>()
            where TAbstraction : class
            where TImplementation : class, TAbstraction;
        
        public abstract IServiceRegistry BindAsTransientToSelf<TAbstraction, TImplementation>()
            where TAbstraction : class
            where TImplementation : class, TAbstraction;
        
        public abstract IServiceRegistry BindAsTransientToSelf<TImplementation>()
            where TImplementation : class;

        public abstract IServiceRegistry BindAsOperationFactory<TArgument, TImplementation>()
            where TArgument : class
            where TImplementation : class, IOperation<TArgument>;

        public abstract IServiceRegistry BindAsFactory<TData>()
            where TData : class;

        protected abstract IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2>()
            where TAbstraction : class
            where TImplementation1 : class, TAbstraction
            where TImplementation2 : class, TAbstraction, TImplementation1;
        
        protected abstract IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2, TImplementation3>()
            where TAbstraction : class
            where TImplementation1 : class, TAbstraction
            where TImplementation2 : class, TAbstraction, TImplementation1
            where TImplementation3 : class, TAbstraction, TImplementation1, TImplementation2;
        
        protected abstract IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2, TImplementation3, TImplementation4>()
            where TAbstraction : class
            where TImplementation1 : class, TAbstraction
            where TImplementation2 : class, TAbstraction, TImplementation1
            where TImplementation3 : class, TAbstraction, TImplementation1, TImplementation2
            where TImplementation4 : class, TAbstraction, TImplementation1, TImplementation2, TImplementation3;
        
        protected abstract IServiceRegistry BindAsSingleton<TAbstraction, TImplementation1, TImplementation2, TImplementation3, TImplementation4, TImplementation5>()
            where TAbstraction : class
            where TImplementation1 : class, TAbstraction
            where TImplementation2 : class, TAbstraction, TImplementation1
            where TImplementation3 : class, TAbstraction, TImplementation1, TImplementation2
            where TImplementation4 : class, TAbstraction, TImplementation1, TImplementation2, TImplementation3
            where TImplementation5 : class, TAbstraction, TImplementation1, TImplementation2, TImplementation3, TImplementation4;
    }
}