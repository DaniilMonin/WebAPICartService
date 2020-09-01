using System;
using System.Data;
using Dapper;

namespace ProtoCart.Data.Database.Mappers
{
    internal sealed class DateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.String:
                    parameter.Value = value.UtcDateTime;
                    break;
                default:
                    throw new InvalidOperationException("DateTimeOffset must be assigned to a Text field.");
            }
        }

        public override DateTimeOffset Parse(object value)
        {
            switch (value)
            {
                case string text:
                    return DateTimeOffset.Parse(text);
                case DateTime time:
                    return new DateTimeOffset(DateTime.SpecifyKind(time, DateTimeKind.Utc), TimeSpan.Zero);
                case DateTimeOffset dto:
                    return dto;
                default:
                    throw new InvalidOperationException("Must be DateTime or DateTimeOffset or Text object to be mapped.");
            }
        }
        
    }
}