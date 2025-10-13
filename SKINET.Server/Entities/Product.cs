namespace SKINET.Server.Entities
{
    public class Product : BaseEntity
    {
        public required  string Name { get; set; }
        public required string Description { get; set; } 
        public decimal Price { get; set; } 
        public required string PictureUrl { get; set; }
        public required  string type { get; set; } 
        public required string Brand { get; set; }  
        public int QuantityInstock { get; set;} = 0;


    }
}
