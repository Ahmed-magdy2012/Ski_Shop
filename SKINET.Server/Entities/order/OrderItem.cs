namespace SKINET.Server.Entities.order
{
    public class  OrderItem :BaseEntity
    {
        public ProductItem Item { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
