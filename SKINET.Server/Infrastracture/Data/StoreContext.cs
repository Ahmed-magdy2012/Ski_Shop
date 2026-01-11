using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Infrastracture.Config;

namespace SKINET.Server.Infrastracture.Data

{
    public class StoreContext(DbContextOptions options) : IdentityDbContext<AppUser>(options )
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        }
    }
}
