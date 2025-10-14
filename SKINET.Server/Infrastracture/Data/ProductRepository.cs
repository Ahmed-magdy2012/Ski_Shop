using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using System.Net.Sockets;

namespace SKINET.Server.Infrastracture.Data
{
    public class ProductRepository(StoreContext context) : IProductRepository
    {
        public void addProduct(Product product)
        {
            context.Products.Add(product);

      }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
          }

        public async Task<IReadOnlyList<string>> GetBrands()
        {
            return await context.Products.Select(x=> x.Brand).Distinct().ToListAsync();

        }

        public async Task<Product?> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async  Task<IReadOnlyList<Product>> GetProducts(string? brand, string? type,string? sort)
        {

            var query= context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(brand)) 
                query=query.Where(x => x.Brand == brand);
            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(x => x.Type == type);
    

            if (!string.IsNullOrWhiteSpace(sort))
                query = sort switch
                {
                    "priceAsc" => query.OrderBy(x => x.Price),
                     "priceDesc" => query.OrderByDescending(x => x.Price),
                     _=> query.OrderBy(x => x.Name)
                };
            return await query.ToListAsync(); 
        }



        public async Task<IReadOnlyList<string>> GetTyps()
        {
            return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }

        public async Task<bool> saveChanges()
        {
            return await context.SaveChangesAsync() > 0 ;
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }

    }
}
