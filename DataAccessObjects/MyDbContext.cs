using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MONKEY5.BusinessObjects;
using System.IO;

namespace MONKEY5.DataAccessObjects
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // Parameterless constructor for use in DAOs
        public MyDbContext()
        {
        }

        // Constructor for dependency injection
        public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration) 
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // If options aren't configured (e.g., when using parameterless constructor),
                // configure from appsettings.json
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:MyDB"];
        }

        // ✅ Define database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<CompletionReport> CompletionReports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }

        // To be improved
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Seed initial data
            base.OnModelCreating(modelBuilder);
        }
    }
}
