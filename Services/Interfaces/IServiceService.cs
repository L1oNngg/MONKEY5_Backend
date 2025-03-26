using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IServiceService
    {
        List<Service> GetServices();
        void SaveService(Service service);
        void UpdateService(Service service);
        void DeleteService(Service service);
        Service? GetServiceById(Guid id);
        List<Service> SearchServicesByName(string name);
        List<Service> GetServicesByPriceRange(decimal minPrice, decimal maxPrice);
    }
}
