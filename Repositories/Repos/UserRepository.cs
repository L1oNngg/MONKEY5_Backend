using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace MONKEY5.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context) { }

        public async Task<User?> GetUserByPhoneAsync(string PhoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber);
        }
    }
}
