using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Infrastracture.Data;

namespace SKINET.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Updateproduct(int id, Product item)
        {
            if (item.Id != id || !Productexits(id)) return BadRequest("cannot Update");
            context.Entry(item).State = EntityState.Modified;   
            await context.SaveChangesAsync();
            return NoContent();


        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Deleteproduct(int id, Product item)
        {
            var product= await context.Products.FindAsync(id);
            if (product == null) return NotFound(); 
            context.Products.Remove(product);
            await context.SaveChangesAsync();   
            return NoContent();
        }

        private bool Productexits(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }
    }
}