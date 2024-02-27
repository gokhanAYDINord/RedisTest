using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisTest.DataAccess.Entities;

namespace RedisTest.DataAccess.Map
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(i => i.CustomerID);            

            builder.Property(i => i.CustomerID).IsRequired(true).HasColumnName("CustomerID");
            builder.Property(i => i.CompanyName).IsRequired(true).HasColumnName("CompanyName");
            builder.Property(i => i.ContactName).IsRequired(true).HasColumnName("ContactName");
            builder.Property(i => i.ContactTitle).IsRequired(true).HasColumnName("ContactTitle");
            builder.Property(i => i.Address).IsRequired(true).HasColumnName("Address");
            builder.Property(i => i.City).IsRequired(true).HasColumnName("City");
            builder.Property(i => i.Region).IsRequired(true).HasColumnName("Region");
            builder.Property(i => i.PostalCode).IsRequired(true).HasColumnName("PostalCode");
            builder.Property(i => i.Country).IsRequired(true).HasColumnName("Country");
            builder.Property(i => i.Phone).IsRequired(true).HasColumnName("Phone");
            builder.Property(i => i.Fax).IsRequired(true).HasColumnName("Fax");
            builder.Property(i => i.Sex).IsRequired(true).HasColumnName("Sex");
        }
    }
}
