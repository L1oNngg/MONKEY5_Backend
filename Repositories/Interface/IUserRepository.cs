using MONKEY5.BusinessObjects;

namespace MONKEY5.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByPhoneAsync(string PhoneNumber);
    }
}
