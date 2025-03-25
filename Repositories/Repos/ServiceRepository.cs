using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public List<Service> GetServices() => ServiceDAO.GetServices();
        
        public void SaveService(Service service) => ServiceDAO.SaveService(service);
        
        public void UpdateService(Service service) => ServiceDAO.UpdateService(service);
        
        public void DeleteService(Service service) => ServiceDAO.DeleteService(service);
        
        public Service? GetServiceById(Guid id) => ServiceDAO.GetServiceById(id);
        
        public List<Service> SearchServicesByName(string name) => 
            ServiceDAO.SearchServicesByName(name);
        
        public List<Service> GetServicesByPriceRange(decimal minPrice, decimal maxPrice) => 
            ServiceDAO.GetServicesByPriceRange(minPrice, maxPrice);
    }
}
