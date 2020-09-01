namespace ProtoCart.Data.Common.Entities
{
    public sealed class Product : UniqueEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool ForBonusPoints { get; set; }
    }
}