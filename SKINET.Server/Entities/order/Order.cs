namespace SKINET.Server.Entities.order
{
    public class Order: BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public required string BuyerEmail { get; set; }
        public ShippingAddress shippingAddress { get; set; } = null!;
        public Deliverymethod Deliverymethod { get; set; }=  null!;
        public PaymentSummary Payment { get; set; }= null!;
        public List<OrderItem> orderItems { get; set; }
        public decimal subtotal { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public required  string PaymentId { get; set; }

        public decimal GetTotal()
        {
            return subtotal + Deliverymethod.price;
        }





    }
}
