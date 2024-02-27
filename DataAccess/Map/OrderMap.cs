using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisTest.DataAccess.Entities;

namespace RedisTest.DataAccess.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(t => t.OrderId);

            builder.Property(i => i.OrderId).IsRequired(true).HasColumnName("OrderId");
            builder.Property(i => i.CustomerID).IsRequired(true).HasColumnName("CustomerID");
            builder.Property(i => i.ShipCity).IsRequired(false).HasColumnName("ShipCity");
            builder.Property(i => i.ShipCountry).IsRequired(false).HasColumnName("ShipCountry");
            builder.Property(i => i.OrderDate).IsRequired(true).HasColumnName("OrderDate");
        }
    }
}
