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

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Seed initial data
            base.OnModelCreating(modelBuilder);
        }
    }
}
