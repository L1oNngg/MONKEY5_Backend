using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace MONKEY5.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(MyDbContext context) : base(context) { }

        public async Task<Staff?> GetStaffByIdAsync(Guid id)
        {
            return await _context.Staffs.FirstOrDefaultAsync(s => s.UserId == id);
        }

        public async Task<Staff?> GetStaffByPhoneAsync(string phone)
        {
            return await _context.Staffs.FirstOrDefaultAsync(s => s.PhoneNumber == phone);
        }

        public async Task<IEnumerable<Staff>> GetAvailableStaffsAsync()
        {
            return await _context.Staffs
                .Where(s => s.Status == BusinessObjects.Helpers.AvailabilityStatus.Available)
                .ToListAsync();
        }
    }
}
