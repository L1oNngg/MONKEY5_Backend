using MONKEY5.BusinessObjects;
using MONKEY5.Repositories;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task RegisterUserAsync(User user)
        {
            user.HashPassword(); // Hash password

            // Gán role mặc định là Customer
            user.Role = Role.Customer;

            await _userRepository.AddUserAsync(user);

            // Nếu user là Customer, thêm vào bảng Customer
            if (user.Role == Role.Customer)
            {
                var customer = new Customer
                {
                    UserId = user.UserId,
                    RegistrationDate = DateTime.UtcNow
                };

                await _customerRepository.AddCustomerAsync(customer);
            }
        }



        public async Task<bool> VerifyPasswordAsync(string enteredPassword, string storedHash)
        {
            return await Task.Run(() => PasswordHasher.VerifyPassword(enteredPassword, storedHash));
        }
    }
}
