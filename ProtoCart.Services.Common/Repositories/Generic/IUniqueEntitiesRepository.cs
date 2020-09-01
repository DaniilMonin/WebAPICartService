using ProtoCart.Data.Common;

namespace ProtoCart.Services.Common.Repositories.Generic
{
    public interface IUniqueEntitiesRepository<TEntity> : IEntitiesRepository<TEntity>
        where TEntity : UniqueEntity
    {

    }
}