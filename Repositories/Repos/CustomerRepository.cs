using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace MONKEY5.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context) { }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.UserId == id);
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
