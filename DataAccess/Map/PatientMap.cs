using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisTest.DataAccess.Entities;

namespace RedisTest.DataAccess.Map
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.HasKey(t => t.Id);

            builder.Property(i => i.Id).IsRequired(true).HasColumnName("Id");
            builder.Property(i => i.Name).IsRequired(true).HasColumnName("Name");
            builder.Property(i => i.Surname).IsRequired(true).HasColumnName("Surname");
            builder.Property(i => i.Birth).IsRequired(true).HasColumnName("Birth");
            builder.Property(i => i.CreateDate).IsRequired(true).HasColumnName("CreateDate");
        }        
    }
}
