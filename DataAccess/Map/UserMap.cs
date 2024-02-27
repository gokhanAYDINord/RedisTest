using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisTest.DataAccess.Entities;

namespace RedisTest.DataAccess.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {       
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired(true).HasColumnName("Id");
            builder.Property(t => t.Name).IsRequired(true).HasColumnName("Name");
            builder.Property(t => t.Surname).IsRequired(true).HasColumnName("Surname");
            builder.Property(t => t.Birth).IsRequired(true).HasColumnName("Birth");
            builder.Property(t => t.CreateDate).IsRequired(true).HasColumnName("CreateDate");
        }
    }
}
