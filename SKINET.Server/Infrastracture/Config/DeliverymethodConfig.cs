using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SKINET.Server.Entities;

namespace SKINET.Server.Infrastracture.Config
{
    public class DeliverymethodConfig : IEntityTypeConfiguration<Deliverymethod>
    {
        public void Configure(EntityTypeBuilder<Deliverymethod> builder)
        {
            builder.Property(x => x.price).HasColumnType("decimal(18,2)");
               }
    }
}
