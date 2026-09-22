using SKINET.Server.Entities;
using SKINET.Server.Entities.order;

namespace SKINET.Server.DTOS
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public required string BuyerEmail { get; set; }
        public required ShippingAddress shippingAddress { get; set; }
        public required string Deliverymethod { get; set; }
        public required PaymentSummary Payment { get; set; } = null!;
        public required List<OrderItemDto> orderItems { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public decimal shippingPrice { get; set; }
        public required string status { get; set; }
        public required string PaymentId { get; set; }
    }
}
   
