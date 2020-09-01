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
    public interface IServiceRegistry
    {
        IServiceRegistry BindSettings<TSettingsProvider>()
            where TSettingsProvider : class, ISettingsService;

        IServiceRegistry BindLogWriter<TLogWriter>()
            where TLogWriter : class, ILogWriter;

        IServiceRegistry BindTemplatingService<TTemplatingService>()
            where TTemplatingService : class, ITemplatingService;
        
        IServiceRegistry BindSchedulerJobService<TSchedulerJobService>()
            where TSchedulerJobService : class, ISchedulerJobService;

        IServiceRegistry BindValidators<TArgument, TValidatorItem>()
            where TArgument : class
            where TValidatorItem : class, IValidatorPolicyItem<TArgument>;

        IServiceRegistry BindValidators<TArgument, TValidatorItem1, TValidatorItem2>()
            where TArgument : class
            where TValidatorItem1 : class, IValidatorPolicyItem<TArgument>
            where TValidatorItem2 : class, IValidatorPolicyItem<TArgument>;

        IServiceRegistry BindValidators<TArgument, TValidatorItem1, TValidatorItem2, TValidatorItem3>()
            where TArgument : class
            where TValidatorItem1 : class, IValidatorPolicyItem<TArgument>
            where TValidatorItem2 : class, IValidatorPolicyItem<TArgument>
            where TValidatorItem3 : class, IValidatorPolicyItem<TArgument>;

        IServiceRegistry BindRepository<TEntity, TRepositoryAbstraction, TRepository>()
            where TEntity : Entity
            where TRepositoryAbstraction : class, IEntitiesRepository<TEntity>
            where TRepository : class, IEntitiesRepository<TEntity>, TRepositoryAbstraction;

        IServiceRegistry BindUniqueRepository<TEntity, TRepositoryAbstraction, TUniqueEntityRepository>()
            where TEntity : UniqueEntity
            where TRepositoryAbstraction : class, IUniqueEntitiesRepository<TEntity>
            where TUniqueEntityRepository : class, IUniqueEntitiesRepository<TEntity>, TRepositoryAbstraction;

        IServiceRegistry BindUniqueReadOnlyRepository<TEntity, TRepositoryAbstraction, TUniqueEntityRepository>()
            where TEntity : UniqueEntity
            where TRepositoryAbstraction : class, IReadOnlyEntitiesRepository<TEntity>
            where TUniqueEntityRepository : class, IUniqueEntitiesRepository<TEntity>, TRepositoryAbstraction;

        IServiceRegistry BindUniqueExtendedRepository<TEntity, TRepositoryAbstraction, TUniqueEntityRepository>()
            where TEntity : UniqueEntity
            where TRepositoryAbstraction : class, IUniqueEntitiesExtendedRepository<TEntity>
            where TUniqueEntityRepository : class, IUniqueEntitiesExtendedRepository<TEntity>, TRepositoryAbstraction;

        IServiceRegistry BindAsSingleton<TAbstraction, TImplementation>()
            where TAbstraction : class
            where TImplementation : class, TAbstraction;
        
        IServiceRegistry BindAsTransientToSelf<TAbstraction, TImplementation>()
            where TAbstraction : class
            where TImplementation : class, TAbstraction;
        
        IServiceRegistry BindAsTransientToSelf<TImplementation>()
            where TImplementation : class;

        IServiceRegistry BindAsOperationFactory<TArgument, TImplementation>()
            where TArgument : class
            where TImplementation : class, IOperation<TArgument>;
    }
}