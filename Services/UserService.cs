using MONKEY5.BusinessObjects;
using MONKEY5.Repositories;

namespace MONKEY5.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();  // Fetch all users
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);  // Fetch user by ID
        }

        public async Task<User?> GetUserByPhoneAsync(string phoneNumber)
        {
            return await _userRepository.GetUserByPhoneAsync(phoneNumber);  // Fetch user by phone
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();  // Save changes
        }

        public async Task UpdateUserAsync(User user)
        {
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();  // Save changes
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                await _userRepository.SaveChangesAsync();  // Save changes
            }
        }
    }
}
