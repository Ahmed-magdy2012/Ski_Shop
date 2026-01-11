using Microsoft.AspNetCore.Identity;

namespace SKINET.Server.Entities
{
    public class AppUser:IdentityUser
    {
        public string? Firstname { get; set; }
        public string? LastName { get; set; }

        public Address? address { get; set; }


    }
}
