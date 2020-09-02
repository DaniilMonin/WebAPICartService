namespace ProtoCart.Data.Database.Helpers
{
    internal static class DatabaseHelper
    {
        public const string ProductTableName = "Products";
        public const string CartTableName = "Carts";
        public const string CartToProductLinksTableName = "CartLinks";
        public const string JobTableName = "Jobs";
        public const string HooksTableName = "Hooks";
        public const string PeriodReportsTableName = "DailyCartReports";
        public const string TemplateReportsTableName = "TemplateReports";
        
        public const string IdColumnName = "Id";
        public const string CartIdLinkTableColumnName = "CartId";
        public const string ProductIdLinkTableColumnName = "ProductId";
        public const string ServiceIdHookColumnName = "ServiceId";
        public const string LastUpdateStampColumnName = "LastUpdateStamp";
        
    }
}