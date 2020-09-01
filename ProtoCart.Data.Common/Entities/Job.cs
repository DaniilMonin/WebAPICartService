namespace ProtoCart.Data.Common.Entities
{
    public sealed class Job : UniqueEntity
    {
        public string JobId { get; set; }
        
        public string Cron { get; set; }
        
        public string Comment { get; set; }
    }
}