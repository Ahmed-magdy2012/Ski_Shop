 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Infrastracture.Data;

namespace SKINET.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? kind,string? sort)
        {
            return Ok(await repo.GetProducts(brand, kind,sort));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductById(id);
            if (product == null) return  NotFound();
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            repo.addProduct(product);
           if (await repo.saveChanges())
            {
                return CreatedAtAction("GetProduct", new {id=product.Id,product});
            } 
            return BadRequest("problem in creating product");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Updateproduct(int id, Product item)
        {
            if (item.Id != id || !Productexits(id)) return BadRequest("cannot Update");
            repo.UpdateProduct(item); 
            if(await repo.saveChanges())
                return NoContent(); 
            
            return BadRequest("problem updating the product");


        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Deleteproduct(int id, Product item)
        {
          var product= await  repo.GetProductById(id);
            if (product == null) return NotFound(); 
            repo.DeleteProduct(product);
            if (await repo.saveChanges())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id, product });
            }
            return BadRequest("problem Deleting the product");
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {

            return Ok(await repo.GetBrands()); 

        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {

            return Ok(await repo.GetBrands());

        }

        private bool Productexits(int id)
        {
            return repo.ProductExists(id);
        }

    }
}