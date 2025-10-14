using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;

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

        public async Task<Product?> GetProductById(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async  Task<IReadOnlyList<Product>> GetProducts()
        {
            return await context.Products.ToListAsync();
                
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
