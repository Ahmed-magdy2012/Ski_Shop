using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKINET.Server.DTOS;
using SKINET.Server.Entities;
using SKINET.Server.NewFolder;
using System.Security.Claims;

namespace SKINET.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<AppUser> _signIn) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Firstname = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email


            };
            var result = await _signIn.UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);

                }
                return ValidationProblem();
            }
            return Ok();
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return NoContent();
        }

        [HttpGet("user_info")]
        public async Task<ActionResult> GetUserInfo()
        {
           // var user1 = await _signIn.UserManager.GetUserAsync(User);

            if (User.Identity?.IsAuthenticated == false) return NoContent();


            var user = await _signIn.UserManager.GetUserbyemail(User);

            if (user == null) return Unauthorized();


            return Ok(new
            {
                user.Email,
                user.Firstname,
                user.LastName

            });




        }
        [HttpGet]
        public  ActionResult GetAuthState()
        {

            return Ok(new
            {
             IsAuthenticated=User.Identity?.IsAuthenticated??false

            });
        }
    }
}