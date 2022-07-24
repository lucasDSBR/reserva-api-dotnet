namespace Api.Domain.Entities
{
    public class ItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public string PathPhoto { get; set; }
        public string Description { get; set; }
        public bool Fee { get; set; }
        public double ValueFee { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}