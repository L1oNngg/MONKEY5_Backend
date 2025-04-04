using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.DTOs;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        void SaveCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Customer GetCustomerById(Guid id);
        Customer GetCustomerByEmail(string email);
        Customer? Login(string email, string password);
        ICollection<LocationDTO> GetCustomerLocations(Guid customerId);
        void AddLocationToCustomer(Guid customerId, LocationDTO locationDto);
    }
}
