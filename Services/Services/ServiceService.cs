using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;

        public ServiceService()
        {
            serviceRepository = new ServiceRepository();
        }

        public List<Service> GetServices() => serviceRepository.GetServices();
        
        public void SaveService(Service service) => serviceRepository.SaveService(service);
        
        public void UpdateService(Service service) => serviceRepository.UpdateService(service);
        
        public void DeleteService(Service service) => serviceRepository.DeleteService(service);
        
        public Service? GetServiceById(Guid id) => serviceRepository.GetServiceById(id);
        
        public List<Service> SearchServicesByName(string name) => 
            serviceRepository.SearchServicesByName(name);
        
        public List<Service> GetServicesByPriceRange(decimal minPrice, decimal maxPrice) => 
            serviceRepository.GetServicesByPriceRange(minPrice, maxPrice);
    }
}
