using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SKINET.Server.DTOS;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Specifictions;
using System.Security.Claims;

namespace SKINET.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized() { 
        return Unauthorized();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("internalError")]
        public IActionResult GetinternalError()
        {
           throw new  Exception("this a test exception");
        }
        [HttpPost("validation")]
        public IActionResult GetValidation( CreateProductDto product)
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("secret")]
        public IActionResult Getsecretn()
        {
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(name + id);
        }

    }
}
