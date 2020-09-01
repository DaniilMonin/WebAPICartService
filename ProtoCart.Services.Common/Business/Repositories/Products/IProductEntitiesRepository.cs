using System.Threading.Tasks;
using ProtoCart.Data.Common;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Services.Common.Repositories.Generic;

namespace ProtoCart.Services.Common.Business.Repositories.Products
{
    public interface IProductEntitiesRepository : IUniqueEntitiesExtendedRepository<Product>
    {
        
    }
}