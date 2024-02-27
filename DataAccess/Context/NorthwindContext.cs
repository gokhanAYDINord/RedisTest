using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedisTest.DataAccess.Entities;
using RedisTest.DataAccess.Map;
using System;

namespace RedisTest.DataAccess.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> dbContextOptions) :base()
        {

        }
        public NorthwindContext()//:base("NorthwindContext")
        {
            //her seferinde yeniden oluşturur.
            //Database.SetInitializer<NorthwindContext>(new DropCreateDatabaseAlways<NorthwindContext>());
            //Database.SetInitializer<NorthwindContext>(new DropCreateDatabaseIfModelChanges<NorthwindContext>());
            //Database.SetInitializer<NorthwindContext>(new MigrateDatabaseToLatestVersion<NorthwindContext, NorthwindEfCodeFirstFluentApi.Migrations.Configuration>());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ArcConnection"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database.SetInitializer<NorthwindContext>(new DropCreateDatabaseAlways<NorthwindContext>());
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Entities.Order> Order { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Patient> Patient { get; set; }
    }
}
