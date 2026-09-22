namespace SKINET.Server.DTOS
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public required string PictureUrl { get; set; }
        public int Quantity { get; set; }
    }
}
