using MONKEY5.BusinessObjects;

namespace MONKEY5.Repositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<Service?> GetServiceByIdAsync(Guid id);
        Task<List<Service>> SearchServicesByNameAsync(string name);
        Task<List<Service>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
