namespace SKINET.Server.Entities
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public required string PropductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string PictureUrl { get; set; }
        public required string Brand { get; set; }
        public required string Type { get; set; }

    }
}
