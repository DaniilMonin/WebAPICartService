using System;

namespace ProtoCart.Data.Common.Entities
{
    public sealed class PeriodCartReport : Entity
    {
        public string Body { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}