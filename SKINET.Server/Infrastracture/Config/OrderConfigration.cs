using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SKINET.Server.Entities.order;

namespace SKINET.Server.Infrastracture.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.shippingAddress,o=>o.WithOwner());
            builder.OwnsOne(x => x.Payment, o => o.WithOwner());
            builder.Property(x => x.status).HasConversion(o=>o.ToString(),
                 o=>(OrderStatus)Enum.Parse(typeof(OrderStatus),o));
            builder.HasMany(x=>x.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
