using BusinessObjects;
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
                optionsBuilder.UseNpgsql(GetConnectionString());
            }

            // Suppress the warning about model changes
            optionsBuilder.ConfigureWarnings(warnings => 
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
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
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure inheritance relationships using TPT (Table Per Type)
            // This creates separate tables for User, Customer, Staff, and Manager
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Manager>().ToTable("Managers");

            // Store the enum as a string
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(),  // Enum -> String (Save to DB)
                    v => (Role)Enum.Parse(typeof(Role), v) // String -> Enum (Read from DB)
                );

            // Customer has 0..1 Avatar
            modelBuilder.Entity<Avatar>()
                .HasOne(a => a.Customer)
                .WithOne()
                .HasForeignKey<Avatar>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // Customer 1 has 0..N Locations
            modelBuilder.Entity<Location>()
                .HasOne<Customer>()
                .WithMany(c => c.Locations)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer 1 places 0..N Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Location)
                .WithMany()
                .HasForeignKey(b => b.LocationId)
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

            // Staff 1 requests 0..N LeaveRequest
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Staff)
                .WithMany()
                .HasForeignKey(l => l.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // // Configure required fields and other constraints
            // modelBuilder.Entity<User>()
            //     .Property(u => u.Email)
            //     .IsRequired();

            // modelBuilder.Entity<User>()
            //     .Property(u => u.PasswordHash)
            //     .IsRequired();

            // modelBuilder.Entity<User>()
            //     .Property(u => u.PhoneNumber)
            //     .IsRequired();

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

            // Seed data
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Locations (Vietnam)
            var locations = new List<Location>
            {
                // Customer 1's locations
                new Location
                {
                    LocationId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Address = "123 Nguyen Hue Street",
                    City = "Ho Chi Minh City",
                    Country = "Vietnam",
                    PostalCode = "70000",
                    CustomerId = Guid.Parse("40000000-0000-0000-0000-000000000001") // Customer 1
                },
                new Location
                {
                    LocationId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    Address = "456 Le Loi Street",
                    City = "Hanoi",
                    Country = "Vietnam",
                    PostalCode = "10000",
                    CustomerId = Guid.Parse("40000000-0000-0000-0000-000000000001") // Customer 1
                },
                
                // Customer 2's locations
                new Location
                {
                    LocationId = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    Address = "789 Tran Hung Dao Street",
                    City = "Da Nang",
                    Country = "Vietnam",
                    PostalCode = "50000",
                    CustomerId = Guid.Parse("40000000-0000-0000-0000-000000000002") // Customer 2
                },
                new Location
                {
                    LocationId = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    Address = "101 Ba Trieu Street",
                    City = "Hai Phong",
                    Country = "Vietnam",
                    PostalCode = "18000",
                    CustomerId = Guid.Parse("40000000-0000-0000-0000-000000000002") // Customer 2
                },
                
                // Customer 3's locations
                new Location
                {
                    LocationId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Address = "202 Le Duan Street",
                    City = "Can Tho",
                    Country = "Vietnam",
                    PostalCode = "90000",
                    CustomerId = Guid.Parse("40000000-0000-0000-0000-000000000003") // Customer 3
                },
                new Location
                {
                    LocationId = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    Address = "303 Quang Trung Street",
                    City = "Nha Trang",
                    Country = "Vietnam",
                    PostalCode = "65000",
                    CustomerId = Guid.Parse("40000000-0000-0000-0000-000000000003") // Customer 3
                }
            };

            modelBuilder.Entity<Location>().HasData(locations);

            // Seed Services
            var services = new List<Service>
            {
                new Service
                {
                    ServiceId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ServiceName = "Cleaning",
                    Description = "Professional home cleaning service",
                    UnitPrice = 90000M,
                    UnitType = "hour"
                },
                new Service
                {
                    ServiceId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ServiceName = "Child care (1 child)",
                    Description = "Childcare service for one child",
                    UnitPrice = 150000M,
                    UnitType = "hour"
                },
                new Service
                {
                    ServiceId = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    ServiceName = "Child care (2 children)",
                    Description = "Childcare service for two children",
                    UnitPrice = 200000M,
                    UnitType = "hour"
                },
                new Service
                {
                    ServiceId = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    ServiceName = "Cooking",
                    Description = "Professional cooking service",
                    UnitPrice = 80000M,
                    UnitType = "dish"
                }
            };

            modelBuilder.Entity<Service>().HasData(services);

            // Seed Manager (admin)
            var adminManager = new Manager
            {
                UserId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                FullName = "Admin User",
                Email = "admin@monkey5.com",
                PasswordHash = PasswordHasher.HashPassword("admin"), // Hash the password
                PhoneNumber = "0123456789",
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Male",
                IdNumber = "123456789",
                Role = MONKEY5.BusinessObjects.Helpers.Role.Manager
            };

            modelBuilder.Entity<Manager>().HasData(adminManager);

            // Seed Customers
            var customers = new List<Customer>
            {
                new Customer
                {
                    UserId = Guid.Parse("40000000-0000-0000-0000-000000000001"),
                    FullName = "Nguyen Van A",
                    Email = "nguyenvana@example.com",
                    PasswordHash = PasswordHasher.HashPassword("customer"),
                    PhoneNumber = "0123456781",
                    DateOfBirth = new DateTime(1985, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Male",
                    IdNumber = "123456789012",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Customer,
                    RegistrationDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Customer
                {
                    UserId = Guid.Parse("40000000-0000-0000-0000-000000000002"),
                    FullName = "Tran Thi B",
                    Email = "tranthib@example.com",
                    PasswordHash = PasswordHasher.HashPassword("customer"),
                    PhoneNumber = "0123456782",
                    DateOfBirth = new DateTime(1990, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Female",
                    IdNumber = "234567890123",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Customer,
                    RegistrationDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Customer
                {
                    UserId = Guid.Parse("40000000-0000-0000-0000-000000000003"),
                    FullName = "Le Van C",
                    Email = "levanc@example.com",
                    PasswordHash = PasswordHasher.HashPassword("customer"),
                    PhoneNumber = "0123456783",
                    DateOfBirth = new DateTime(1988, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Male",
                    IdNumber = "345678901234",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Customer,
                    RegistrationDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            modelBuilder.Entity<Customer>().HasData(customers);

            // Seed Staffs (2 for each service)
            var staffs = new List<Staff>
            {
                // Cleaning staff
                new Staff
                {
                    UserId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    FullName = "Pham Thi D",
                    Email = "phamthid@example.com",
                    PasswordHash = PasswordHasher.HashPassword("staff"),
                    PhoneNumber = "0234567891",
                    DateOfBirth = new DateTime(1992, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Female",
                    IdNumber = "456789012345",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Staff,
                    AvgRating = 4.5,
                    Status = MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available
                },
                new Staff
                {
                    UserId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    FullName = "Hoang Van E",
                    Email = "hoangvane@example.com",
                    PasswordHash = PasswordHasher.HashPassword("staff"),
                    PhoneNumber = "0234567892",
                    DateOfBirth = new DateTime(1991, 7, 15, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Male",
                    IdNumber = "567890123456",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Staff,
                    AvgRating = 4.2,
                    Status = MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available
                },
                // Childcare staff
                new Staff
                {
                    UserId = Guid.Parse("50000000-0000-0000-0000-000000000003"),
                    FullName = "Nguyen Thi F",
                    Email = "nguyenthif@example.com",
                    PasswordHash = PasswordHasher.HashPassword("staff"),
                    PhoneNumber = "0234567893",
                    DateOfBirth = new DateTime(1989, 9, 5, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Female",
                    IdNumber = "678901234567",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Staff,
                    AvgRating = 4.8,
                    Status = MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available
                },
                new Staff
                {
                    UserId = Guid.Parse("50000000-0000-0000-0000-000000000004"),
                    FullName = "Tran Van G",
                    Email = "tranvang@example.com",
                    PasswordHash = PasswordHasher.HashPassword("staff"),
                    PhoneNumber = "0234567894",
                    DateOfBirth = new DateTime(1993, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Male",
                    IdNumber = "789012345678",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Staff,
                    AvgRating = 4.3,
                    Status = MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available
                },
                // Cooking staff
                new Staff
                {
                    UserId = Guid.Parse("50000000-0000-0000-0000-000000000005"),
                    FullName = "Le Thi H",
                    Email = "lethih@example.com",
                    PasswordHash = PasswordHasher.HashPassword("staff"),
                    PhoneNumber = "0234567895",
                    DateOfBirth = new DateTime(1987, 11, 30, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Female",
                    IdNumber = "890123456789",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Staff,
                    AvgRating = 4.9,
                    Status = MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available
                },
                new Staff
                {
                    UserId = Guid.Parse("50000000-0000-0000-0000-000000000006"),
                    FullName = "Pham Van I",
                    Email = "phamvani@example.com",
                    PasswordHash = PasswordHasher.HashPassword("staff"),
                    PhoneNumber = "0234567896",
                    DateOfBirth = new DateTime(1990, 6, 12, 0, 0, 0, DateTimeKind.Utc),
                    Gender = "Male",
                    IdNumber = "901234567890",
                    Role = MONKEY5.BusinessObjects.Helpers.Role.Staff,
                    AvgRating = 4.6,
                    Status = MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available
                }
            };

            modelBuilder.Entity<Staff>().HasData(staffs);

            // Sample Bookings
            var bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = Guid.Parse("60000000-0000-0000-0000-000000000001"),
                    CustomerId = customers[0].UserId,
                    StaffId = staffs[0].UserId, // Cleaning staff
                    ServiceId = services[0].ServiceId, // Cleaning service
                    Status = MONKEY5.BusinessObjects.Helpers.OrderStatus.Completed,
                    BookingDateTime = new DateTime(2025, 3, 2, 0, 0, 0, DateTimeKind.Utc),
                    ServiceStartTime = new DateTime(2025, 3, 3, 10, 0, 0, DateTimeKind.Utc),
                    ServiceEndTime = new DateTime(2025, 3, 3, 13, 0, 0, DateTimeKind.Utc),
                    ServiceUnitAmount = 3, // 3 hours
                    TotalPrice = 270000f, // 90,000 x 3
                    Note = "Please focus on kitchen and bathroom cleaning. Customer has a cat, so be careful when entering.",
                    LocationId = locations[0].LocationId // Using the first location for Customer 1
                },
                new Booking
                {
                    BookingId = Guid.Parse("60000000-0000-0000-0000-000000000002"),
                    CustomerId = customers[1].UserId,
                    StaffId = staffs[2].UserId, // Childcare staff
                    ServiceId = services[1].ServiceId, // Childcare (1 child)
                    Status = MONKEY5.BusinessObjects.Helpers.OrderStatus.Completed,
                    BookingDateTime = new DateTime(2025, 3, 3, 0, 0, 0, DateTimeKind.Utc),
                    ServiceStartTime = new DateTime(2025, 3, 4, 14, 0, 0, DateTimeKind.Utc),
                    ServiceEndTime = new DateTime(2025, 3, 4, 18, 0, 0, DateTimeKind.Utc),
                    ServiceUnitAmount = 4, // 4 hours
                    TotalPrice = 600000f, // 150,000 x 4
                    Note = "Child is 5 years old and has homework to complete. Allergic to peanuts.",
                    LocationId = locations[2].LocationId // Using the first location for Customer 2
                },
                new Booking
                {
                    BookingId = Guid.Parse("60000000-0000-0000-0000-000000000003"),
                    CustomerId = customers[2].UserId,
                    StaffId = staffs[4].UserId, // Cooking staff
                    ServiceId = services[3].ServiceId, // Cooking service
                    Status = MONKEY5.BusinessObjects.Helpers.OrderStatus.Completed,
                    BookingDateTime = new DateTime(2025, 3, 4, 0, 0, 0, DateTimeKind.Utc),
                    ServiceStartTime = new DateTime(2025, 3, 5, 15, 0, 0, DateTimeKind.Utc),
                    ServiceEndTime = new DateTime(2025, 3, 5, 18, 0, 0, DateTimeKind.Utc),
                    ServiceUnitAmount = 5, // 5 dishes
                    TotalPrice = 400000f, // 80,000 x 5
                    Note = "Family prefers vegetarian dishes. Please use less spicy ingredients.",
                    LocationId = locations[4].LocationId // Using the first location for Customer 3
                },
                new Booking
                {
                    BookingId = Guid.Parse("60000000-0000-0000-0000-000000000004"),
                    CustomerId = customers[0].UserId,
                    StaffId = staffs[1].UserId, // Cleaning staff
                    ServiceId = services[0].ServiceId, // Cleaning service
                    Status = MONKEY5.BusinessObjects.Helpers.OrderStatus.Confirmed,
                    BookingDateTime = new DateTime(2025, 3, 5, 0, 0, 0, DateTimeKind.Utc),
                    ServiceStartTime = new DateTime(2025, 3, 6, 7, 0, 0, DateTimeKind.Utc),
                    ServiceEndTime = new DateTime(2025, 3, 6, 11, 0, 0, DateTimeKind.Utc),
                    ServiceUnitAmount = 4, // 4 hours
                    TotalPrice = 360000f, // 90,000 x 4
                    Note = "Deep cleaning needed for living room. Customer will provide special cleaning products for wooden furniture.",
                    LocationId = locations[1].LocationId // Using the second location for Customer 1
                },
                new Booking
                {
                    BookingId = Guid.Parse("60000000-0000-0000-0000-000000000005"),
                    CustomerId = customers[1].UserId,
                    StaffId = staffs[3].UserId, // Childcare staff
                    ServiceId = services[2].ServiceId, // Childcare (2 children)
                    Status = MONKEY5.BusinessObjects.Helpers.OrderStatus.Confirmed,
                    BookingDateTime = new DateTime(2025, 3, 6, 0, 0, 0, DateTimeKind.Utc),
                    ServiceStartTime = new DateTime(2025, 3, 7, 8, 0, 0, DateTimeKind.Utc),
                    ServiceEndTime = new DateTime(2025, 3, 7, 13, 0, 0, DateTimeKind.Utc),
                    ServiceUnitAmount = 5, // 5 hours
                    TotalPrice = 1000000f, // 200,000 x 5
                    Note = "Two children ages 3 and 6. The older child has online classes from 9-10 AM. Both children need lunch prepared.",
                    LocationId = locations[3].LocationId // Using the second location for Customer 2
                }
            };

            modelBuilder.Entity<Booking>().HasData(bookings);


            // Sample Payments
            var payments = new List<Payment>
            {
                new Payment
                {
                    PaymentId = Guid.Parse("70000000-0000-0000-0000-000000000001"),
                    BookingId = bookings[0].BookingId,
                    Amount = 270000M,
                    PaymentMethod = MONKEY5.BusinessObjects.Helpers.PaymentMethod.CreditCard,
                    PaymentStatus = MONKEY5.BusinessObjects.Helpers.PaymentStatus.Completed,
                    PaymentCreatedAt = bookings[0].BookingDateTime,
                    PaymentPaidAt = bookings[0].BookingDateTime.AddHours(1)
                },
                new Payment
                {
                    PaymentId = Guid.Parse("70000000-0000-0000-0000-000000000002"),
                    BookingId = bookings[1].BookingId,
                    Amount = 600000M,
                    PaymentMethod = MONKEY5.BusinessObjects.Helpers.PaymentMethod.Cash,
                    PaymentStatus = MONKEY5.BusinessObjects.Helpers.PaymentStatus.Completed,
                    PaymentCreatedAt = bookings[1].BookingDateTime,
                    PaymentPaidAt = bookings[1].ServiceEndTime.AddMinutes(30)
                },
                new Payment
                {
                    PaymentId = Guid.Parse("70000000-0000-0000-0000-000000000003"),
                    BookingId = bookings[2].BookingId,
                    Amount = 400000M,
                    PaymentMethod = MONKEY5.BusinessObjects.Helpers.PaymentMethod.BankTransfer,
                    PaymentStatus = MONKEY5.BusinessObjects.Helpers.PaymentStatus.Completed,
                    PaymentCreatedAt = bookings[2].BookingDateTime,
                    PaymentPaidAt = bookings[2].BookingDateTime.AddHours(2)
                },
                new Payment
                {
                    PaymentId = Guid.Parse("70000000-0000-0000-0000-000000000004"),
                    BookingId = bookings[3].BookingId,
                    Amount = 360000M,
                    PaymentMethod = MONKEY5.BusinessObjects.Helpers.PaymentMethod.CreditCard,
                    PaymentStatus = MONKEY5.BusinessObjects.Helpers.PaymentStatus.Completed,
                    PaymentCreatedAt = bookings[3].BookingDateTime,
                    PaymentPaidAt = null
                },
                new Payment
                {
                    PaymentId = Guid.Parse("70000000-0000-0000-0000-000000000005"),
                    BookingId = bookings[4].BookingId,
                    Amount = 1000000M,
                    PaymentMethod = MONKEY5.BusinessObjects.Helpers.PaymentMethod.BankTransfer,
                    PaymentStatus = MONKEY5.BusinessObjects.Helpers.PaymentStatus.Completed,
                    PaymentCreatedAt = bookings[4].BookingDateTime,
                    PaymentPaidAt = null
                }
            };

            modelBuilder.Entity<Payment>().HasData(payments);

            // Sample Reviews
            var reviews = new List<Review>
            {
                new Review
                {
                    ReviewId = Guid.Parse("80000000-0000-0000-0000-000000000001"),
                    BookingId = bookings[0].BookingId,
                    RatingStar = 5,
                    Comment = "Excellent cleaning service, very thorough and professional!",
                    ReviewDateTime = bookings[0].ServiceEndTime.AddHours(2)
                },
                new Review
                {
                    ReviewId = Guid.Parse("80000000-0000-0000-0000-000000000002"),
                    BookingId = bookings[1].BookingId,
                    RatingStar = 4,
                    Comment = "Great childcare service, my child was very happy.",
                    ReviewDateTime = bookings[1].ServiceEndTime.AddHours(3)
                },
                new Review
                {
                    ReviewId = Guid.Parse("80000000-0000-0000-0000-000000000003"),
                    BookingId = bookings[2].BookingId,
                    RatingStar = 5,
                    Comment = "Amazing cooking! The dishes were delicious and beautifully presented.",
                    ReviewDateTime = bookings[2].ServiceEndTime.AddHours(1)
                }
            };

            modelBuilder.Entity<Review>().HasData(reviews);

            // Sample Refund
            var refunds = new List<Refund>
            {
                new Refund
                {
                    RefundId = Guid.Parse("90000000-0000-0000-0000-000000000001"),
                    PaymentId = payments[2].PaymentId,
                    RefundAmount = 100000M, // Partial refund
                    RefundReason = "One dish was not prepared as requested",
                    RefundDateTime = payments[2].PaymentPaidAt.HasValue 
                                    ? payments[2].PaymentPaidAt.Value.AddDays(1)
                                    : DateTime.UtcNow // Provide a fallback value
                }
            };

            modelBuilder.Entity<Refund>().HasData(refunds);

            // Sample Completion Reports
            var completionReports = new List<CompletionReport>
            {
                new CompletionReport
                {
                    ReportId = Guid.Parse("A0000000-0000-0000-0000-000000000001"),
                    BookingId = bookings[0].BookingId,
                    ReportText = "Cleaned living room, kitchen, 2 bathrooms, and 3 bedrooms. All surfaces dusted and floors mopped.",
                    ReportDateTime = bookings[0].ServiceEndTime.AddMinutes(15)
                },
                new CompletionReport
                {
                    ReportId = Guid.Parse("A0000000-0000-0000-0000-000000000002"),
                    BookingId = bookings[1].BookingId,
                    ReportText = "Took care of the child, prepared lunch, helped with homework, and played educational games.",
                    ReportDateTime = bookings[1].ServiceEndTime.AddMinutes(20)
                },
                new CompletionReport
                {
                    ReportId = Guid.Parse("A0000000-0000-0000-0000-000000000003"),
                    BookingId = bookings[2].BookingId,
                    ReportText = "Prepared 5 dishes: spring rolls, pho, grilled chicken, stir-fried vegetables, and mango sticky rice for dessert.",
                    ReportDateTime = bookings[2].ServiceEndTime.AddMinutes(10)
                }
            };

            modelBuilder.Entity<CompletionReport>().HasData(completionReports);

            // Sample Report Images
            var reportImages = new List<ReportImage>
            {
                new ReportImage
                {
                    ReportImageId = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    ReportId = completionReports[0].ReportId,
                    ImagePath = "cleaning_living_room.jpg"
                },
                new ReportImage
                {
                    ReportImageId = Guid.Parse("B0000000-0000-0000-0000-000000000002"),
                    ReportId = completionReports[0].ReportId,
                    ImagePath = "cleaning_kitchen.jpg"
                },
                new ReportImage
                {
                    ReportImageId = Guid.Parse("B0000000-0000-0000-0000-000000000003"),
                    ReportId = completionReports[1].ReportId,
                    ImagePath = "childcare_lunch.jpg"
                },
                new ReportImage
                {
                    ReportImageId = Guid.Parse("B0000000-0000-0000-0000-000000000004"),
                    ReportId = completionReports[2].ReportId,
                    ImagePath = "cooking_spring_rolls.jpg"
                },
                new ReportImage
                {
                    ReportImageId = Guid.Parse("B0000000-0000-0000-0000-000000000005"),
                    ReportId = completionReports[2].ReportId,
                    ImagePath = "cooking_pho.jpg"
                },
                new ReportImage
                {
                    ReportImageId = Guid.Parse("B0000000-0000-0000-0000-000000000006"),
                    ReportId = completionReports[2].ReportId,
                    ImagePath = "cooking_dessert.jpg"
                }
            };

            modelBuilder.Entity<ReportImage>().HasData(reportImages);

            // Sample Leave Requests
            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    RequestId = Guid.Parse("C0000000-0000-0000-0000-000000000001"),
                    StaffId = staffs[0].UserId, // Cleaning staff
                    RequestDate = new DateTime(2025, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                    LeaveStart = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                    LeaveEnd = new DateTime(2025, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    LeaveReasons = "Family vacation planned months in advance."
                },
                new LeaveRequest
                {
                    RequestId = Guid.Parse("C0000000-0000-0000-0000-000000000002"),
                    StaffId = staffs[2].UserId, // Childcare staff
                    RequestDate = new DateTime(2025, 2, 20, 0, 0, 0, DateTimeKind.Utc),
                    LeaveStart = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                    LeaveEnd = new DateTime(2025, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                    LeaveReasons = "Medical appointment and recovery."
                },
                new LeaveRequest
                {
                    RequestId = Guid.Parse("C0000000-0000-0000-0000-000000000003"),
                    StaffId = staffs[4].UserId, // Cooking staff
                    RequestDate = new DateTime(2025, 2, 25, 0, 0, 0, DateTimeKind.Utc),
                    LeaveStart = new DateTime(2025, 4, 5, 0, 0, 0, DateTimeKind.Utc),
                    LeaveEnd = new DateTime(2025, 4, 10, 0, 0, 0, DateTimeKind.Utc),
                    LeaveReasons = "Professional development course on advanced culinary techniques."
                }
            };

            modelBuilder.Entity<LeaveRequest>().HasData(leaveRequests);
        }       
    }
}
