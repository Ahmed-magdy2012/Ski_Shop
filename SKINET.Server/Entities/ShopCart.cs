namespace SKINET.Server.Entities
{
    public class ShopCart
    {
        public required string Id { get; set; }  
        public List<CartItem> Items { get; set; } = [];
        public int? DeliverymethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentmethodId { get; set; }

    }
}


