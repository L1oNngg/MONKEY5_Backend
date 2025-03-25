using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
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
            // Configure the base entity (User) to map to its own table
            modelBuilder.Entity<User>()
                .ToTable("Users");

            // Configure derived entities to map to separate tables
            modelBuilder.Entity<Customer>()
                .ToTable("Customers");

            modelBuilder.Entity<Staff>()
                .ToTable("Staffs");

            modelBuilder.Entity<Manager>()
                .ToTable("Managers");

            // Store the enum as a string
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(),  // Enum -> String (Save to DB)
                    v => (Role)Enum.Parse(typeof(Role), v) // String -> Enum (Read from DB)
                );

            // Configure inheritance relationships using TPT (Table Per Type)
            // This creates separate tables for User, Customer, Staff, and Manager
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Manager>().ToTable("Managers");

            // Customer 1 has 1..N Location
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Location)
                .WithMany()
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer 1 places 0..N Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Staff 1 performs 0..N Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Staff)
                .WithMany()
                .HasForeignKey(b => b.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking 1 has 1 Service
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Service)
                .WithMany()
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking 1 has 0..1 Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Booking)
                .WithOne()
                .HasForeignKey<Review>(r => r.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking 1 has 0..1 Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne()
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Booking 1 has 0..1 CompletionReport
            modelBuilder.Entity<CompletionReport>()
                .HasOne(c => c.Booking)
                .WithOne()
                .HasForeignKey<CompletionReport>(c => c.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment 1 may have 0..1 Refund
            modelBuilder.Entity<Refund>()
                .HasOne(r => r.Payment)
                .WithOne()
                .HasForeignKey<Refund>(r => r.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            // CompletionReport 1 has 1..N ReportImage
            modelBuilder.Entity<CompletionReport>()
                .HasMany(c => c.ReportImages)
                .WithOne(r => r.CompletionReport)
                .HasForeignKey(r => r.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure required fields and other constraints
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .IsRequired();

            // Configure decimal precision for money values
            modelBuilder.Entity<Service>()
                .Property(s => s.UnitPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Refund>()
                .Property(r => r.RefundAmount)
                .HasColumnType("decimal(10,2)");

            // Ensure unique email addresses
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }


    }
}
