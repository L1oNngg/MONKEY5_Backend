using MONKEY5.BusinessObjects;

namespace MONKEY5.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}
