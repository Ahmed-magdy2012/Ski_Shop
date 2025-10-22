 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Entities.Specifictions;
using SKINET.Server.Infrastracture.Data;
using SKINET.Server.RequestHelpers;
using System.Linq.Expressions;

namespace SKINET.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
    
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductParams param)
        {
            var spec = new ProductSpecification(param);
            var products = await repo.ListAsync(spec);
            var count=await repo.count(spec);
            var pagination=new Pagination<Product>(param.Pageindex, param.Pagesize,count,products);

            return Ok(pagination);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductByID(id);
            if (product == null) return  NotFound();
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            repo.Add(product);
           if (await repo.Saveall())
            {
                return CreatedAtAction("GetProduct", new {id=product.Id,product});
            } 
            return BadRequest("problem in creating product");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Updateproduct(int id, Product item)
        {
            if (item.Id != id || !Productexits(id)) return BadRequest("cannot Update");
            repo.Update(item); 
            if(await repo.Saveall())
                return NoContent(); 
            
            return BadRequest("problem updating the product");


        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Deleteproduct(int id, Product item)
        {
          var product= await  repo.GetProductByID(id);
            if (product == null) return NotFound(); 
            repo.Delete(product);
            if (await repo.Saveall())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id, product });
            }
            return BadRequest("problem Deleting the product");
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var Brands = new BrandSpecification();

            return Ok(await repo.ListAsync(Brands));

        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {

            var Types = new ListofTypeSpecification();

            return Ok(await repo.ListAsync(Types));
        }

        private bool Productexits(int id)
        {
            return repo.exists(id);
        }

    }
}