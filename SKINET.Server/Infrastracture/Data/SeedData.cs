using Microsoft.AspNetCore.Identity;
using SKINET.Server.Entities;
using System.Reflection;
using System.Text.Json;

namespace SKINET.Server.Infrastracture.Data
{
    public class SeedData
    {
        public static async Task seeding(StoreContext context,UserManager<AppUser>usermanager)
        {
            if (!usermanager.Users.Any(x => x.UserName == "admin@test.com")) {
                var user = new AppUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                await usermanager.CreateAsync(user,"Pa$$w0rd");
                await usermanager.AddToRoleAsync(user, "Admin");

            }
            var path=Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);     
            if (!context.Products.Any()) {
                var produtsData  = await File.ReadAllTextAsync(path+"@/Data/SeedData/products.json");
                var Products=JsonSerializer.Deserialize<List<Product>>(produtsData);
                context.Products.AddRange(Products);
                await context.SaveChangesAsync();   
            } 

            if (!context.Deliverymethods.Any())
            {
                var dmData = await File.ReadAllTextAsync("Infrastracture/Data/SeedData/delivery.json");
                var Delivery = JsonSerializer.Deserialize<List<Deliverymethod>>(dmData);
                context.Deliverymethods.AddRange(Delivery);
                await context.SaveChangesAsync();
            }
        }
    }
}
