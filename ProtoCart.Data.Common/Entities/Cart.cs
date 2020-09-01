using System;

namespace ProtoCart.Data.Common.Entities
{
    public sealed class Cart : UniqueEntity
    {
        public DateTimeOffset LastUpdateStamp { get; set; }
    }
}