using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using System.Security.Authentication;
using System.Security.Claims;

namespace SKINET.Server.NewFolder
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            if (email == null) throw new AuthenticationException("Email claim not found");
            return email;
        }
        public static async Task<AppUser> GetUserbyemail(this UserManager<AppUser> userManger, ClaimsPrincipal user)
        {
            var Userinfo = await userManger.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (Userinfo == null) throw new AuthenticationException("User not found");

            return Userinfo;

        }


        public static async Task<AppUser> GetUserbyemailwithAddress(this UserManager<AppUser> userManger, ClaimsPrincipal user)
        {
            var Userinfo = await userManger.Users.
                Include(x=>x.address).
                FirstOrDefaultAsync(x => x.Email == user.GetEmail());
            if (Userinfo == null) throw new AuthenticationException("User not found");

            return Userinfo;

        }
    }
}