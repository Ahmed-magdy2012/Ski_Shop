using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SKINET.Server.Entities.order;

namespace SKINET.Server.Infrastracture.Config
{

    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(x => x.Item, o => o.WithOwner());
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

        }
    }
}