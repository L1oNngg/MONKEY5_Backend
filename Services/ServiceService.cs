using MONKEY5.BusinessObjects;
using MONKEY5.Repositories;

namespace MONKEY5.Services
{
    public class ServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _serviceRepository.GetAllAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(Guid id)
        {
            return await _serviceRepository.GetServiceByIdAsync(id);
        }

        public async Task<List<Service>> SearchServicesByNameAsync(string name)
        {
            return await _serviceRepository.SearchServicesByNameAsync(name);
        }

        public async Task<List<Service>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _serviceRepository.GetServicesByPriceRangeAsync(minPrice, maxPrice);
        }

        public async Task AddServiceAsync(Service service)
        {
            await _serviceRepository.AddAsync(service);
            await _serviceRepository.SaveChangesAsync();
        }

        public async Task UpdateServiceAsync(Service service)
        {
            _serviceRepository.Update(service);
            await _serviceRepository.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service != null)
            {
                _serviceRepository.Delete(service);
                await _serviceRepository.SaveChangesAsync();
            }
        }
    }
}
