using MONKEY5.BusinessObjects;

namespace MONKEY5.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task RegisterUserAsync(User user);
        Task<bool> VerifyPasswordAsync(string enteredPassword, string storedHash);
    }
}
