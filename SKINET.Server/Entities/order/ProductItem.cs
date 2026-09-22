namespace SKINET.Server.Entities.order
{
    public class ProductItem
    {

        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
    }
}
