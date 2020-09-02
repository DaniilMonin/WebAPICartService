using System.Collections.Generic;
using ProtoCart.Data.Common.Entities;
using ProtoCart.Data.Common.Requests.Generic;

namespace ProtoCart.Data.Common.Requests
{
    public sealed class CartsSweeperRequest : Request<IReadOnlyCollection<Cart>>
    {
        public int DaysToRemove { get; set; }
    }
}