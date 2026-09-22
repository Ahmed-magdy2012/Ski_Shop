using Microsoft.AspNetCore.Authorization;
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
    public class ProductsController(IUnitOfWork unit) : ControllerBase
    {
    
        
        [HttpGet]
        public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery]ProductParams param)
        {
            Console.WriteLine(param.Search);
            var spec = new ProductSpecification(param);
            var products = await unit.Repository<Product>().ListAsync(spec);
            var count=await unit.Repository<Product>().count(spec);
            var pagination=new Pagination<Product>(param.Pageindex, param.Pagesize,count,products);

            return Ok(pagination);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await unit.Repository<Product>().GetByID(id);
            if (product == null) return  NotFound();
            return product;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProducts(Product product)
        {
            unit.Repository<Product>().Add(product);
           if (await unit.Complete())
            {
                return CreatedAtAction("GetProduct", new {id=product.Id,product});
            } 
            return BadRequest("problem in creating product");
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Updateproduct(int id, Product item)
        {
            if (item.Id != id || !Productexits(id)) return BadRequest("cannot Update");
            unit.Repository<Product>().Update(item); 
            if(await unit.Complete())
                return NoContent(); 
            
            return BadRequest("problem updating the product");


        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Deleteproduct(int id, Product item)
        {
          var product= await unit.Repository<Product>().GetByID(id);
            if (product == null) return NotFound();
            unit.Repository<Product>().Delete(product);
            if (await unit.Complete())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id, product });
            }
            return BadRequest("problem Deleting the product");
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var Brands = new BrandSpecification();

            return Ok(await unit.Repository<Product>().ListAsync(Brands));

        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {

            var Types = new ListofTypeSpecification();

            return Ok(await unit.Repository<Product>().ListAsync(Types));
        }

        private bool Productexits(int id)
        {
            return unit.Repository<Product>().exists(id);
        }

    }
}