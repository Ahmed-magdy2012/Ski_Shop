using SKINET.Server.Entities.order;
using System.ComponentModel.DataAnnotations;

namespace SKINET.Server.DTOS
{
    public class CreateOrderDTO
    {
        [Required]
        public string CartId { get; set; } = string.Empty;

        [Required]
        public int DeliveryMethodId { get; set;}

        [Required]  
        public ShippingAddress address { get; set; }
        [Required]
        public PaymentSummary paymentSummary { get; set; } = null!;
    }
}
