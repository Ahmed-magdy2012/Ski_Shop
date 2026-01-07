using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Infrastracture.NewFolder;

namespace SKINET.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<ShopCart>> GetCartById(string id)
        {
            var cart = await cartService.GetCart(id);
            return Ok(cart ?? new ShopCart { Id = id });

        }


        [HttpPost]
        public async Task<ActionResult<ShopCart>> UpdateCart( ShopCart cart)
        {
            var UpdatedCart = await cartService.SetCart(cart);

            if (UpdatedCart == null) return BadRequest("problem with Cart");
            return UpdatedCart;

        }


        [HttpDelete]
        public async Task<ActionResult<ShopCart>> DeleteCart(string id)
        {
            var result = await cartService.DeletCart(id);

            if (!result) return BadRequest("problem with deleting Cart");
            return Ok( );


        }


    }
}
