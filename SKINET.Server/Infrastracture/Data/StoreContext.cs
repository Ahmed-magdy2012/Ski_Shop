using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities;
using SKINET.Server.Infrastracture.Config;

namespace SKINET.Server.Infrastracture.Data

{
    public class StoreContext(DbContextOptions options) : DbContext(options )
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
        }
    }
}
