using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MONKEY5.BusinessObjects;

namespace MONKEY5.DataAccessObjects
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        // ✅ Inject IConfiguration for connection string management
        public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration) 
            : base(options)
        {
            _configuration = configuration;
        }

        // ✅ Override OnConfiguring to set up the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("MyDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
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
