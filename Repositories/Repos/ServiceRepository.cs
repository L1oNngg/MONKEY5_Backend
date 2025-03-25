using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace MONKEY5.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(MyDbContext context) : base(context) { }

        public async Task<Service?> GetServiceByIdAsync(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == id);
        }

        public async Task<List<Service>> SearchServicesByNameAsync(string name)
        {
            return await _context.Services
                .Where(s => s.ServiceName.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Service>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Services
                .Where(s => s.UnitPrice >= minPrice && s.UnitPrice <= maxPrice)
                .ToListAsync();
        }
    }
}
