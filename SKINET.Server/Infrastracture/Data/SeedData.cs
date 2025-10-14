using SKINET.Server.Entities;
using System.Text.Json;

namespace SKINET.Server.Infrastracture.Data
{
    public class SeedData
    {
        public static async Task seeding(StoreContext context)
        {
            if (!context.Products.Any()) {
                var produtsData
                        = await File.ReadAllTextAsync("../Infrastracture/Data/SeedData/Products.jason");
                var Products=JsonSerializer.Deserialize<List<Product>>(produtsData);
                context.Products.AddRange(Products);
                await context.SaveChangesAsync();   
            }
        }
    }
}
