using System.ComponentModel.DataAnnotations;

namespace SKINET.Server.DTOS
{
    public class CreateProductDto
    {
        [Required]
        public  string Name { get; set; }=string.Empty;
        [Required]
        public  string Description { get; set; }= string.Empty;

        [Range(0.01,double.MaxValue,ErrorMessage ="price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public  string PictureUrl { get; set; }=string.Empty ;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Range(1,int.MaxValue,ErrorMessage ="Quantity In stock must be at least 1")]

        public int QuantityInStock { get; set; }

        [Required]
        public string Brand { get; set; } = string.Empty;
    }
}
