using MONKEY5.BusinessObjects;
using MONKEY5.Repositories;

namespace MONKEY5.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _customerRepository.GetCustomerByEmailAsync(email);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            // Hash the password before saving
            customer.HashPassword();
            customer.Role = MONKEY5.BusinessObjects.Helpers.Role.Customer;

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            // Hash the password if it was changed
            if (!string.IsNullOrEmpty(customer.Password))
            {
                customer.HashPassword();
            }

            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                _customerRepository.Delete(customer);
                await _customerRepository.SaveChangesAsync();
            }
        }
    }
}
