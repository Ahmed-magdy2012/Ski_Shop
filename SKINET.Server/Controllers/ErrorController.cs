using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SKINET.Server.DTOS;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Specifictions;

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
        public IActionResult GetinternalErrord()
        {
           throw new  Exception("this a test exception");
        }
        [HttpPost("validation")]
        public IActionResult GetValidation(CreateProductDto product)
        {
            return Ok();
        }
    }
}
