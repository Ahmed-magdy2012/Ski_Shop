namespace SKINET.Server.Entities
{
    public class Deliverymethod :BaseEntity
    {
        public required string ShortName { get; set; }
        public required string DeliveryTime { get; set; }
        public required string Description { get; set; }
        public decimal price { get; set; }


    }
}
