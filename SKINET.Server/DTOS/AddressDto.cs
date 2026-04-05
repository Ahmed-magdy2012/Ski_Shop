using System.ComponentModel.DataAnnotations;

namespace SKINET.Server.DTOS
{
    public class AddressDto
    {
        [Required]
        public required String Line1 { get; set; } = string.Empty;
    
        public String? Line2 { get; set; } = string.Empty;

        [Required]
        public required String City { get; set; } = string.Empty;
        [Required]

        public required String State { get; set; } = string.Empty;

        [Required]
        public required String PostalCode { get; set; } = string.Empty;


        [Required]
        public required String Country { get; set; } = string.Empty;
 
    }
}
