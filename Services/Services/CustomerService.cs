using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.DTOs;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }

        public List<Customer> GetCustomers() => customerRepository.GetCustomers();
        
        public void SaveCustomer(Customer customer) => customerRepository.SaveCustomer(customer);
        
        public void UpdateCustomer(Customer customer) => customerRepository.UpdateCustomer(customer);
        
        public void DeleteCustomer(Customer customer) => customerRepository.DeleteCustomer(customer);
        
        public Customer GetCustomerById(Guid id) => customerRepository.GetCustomerById(id);
        
        public Customer GetCustomerByEmail(string email) => customerRepository.GetCustomerByEmail(email);

        public Customer? Login(string email, string password) => customerRepository.Login(email, password);

        public ICollection<LocationDTO> GetCustomerLocations(Guid customerId)
        {
            var locations = customerRepository.GetCustomerLocations(customerId);
            return locations.Select(l => new LocationDTO
            {
                LocationId = l.LocationId,
                Address = l.Address,
                City = l.City,
                Country = l.Country,
                PostalCode = l.PostalCode
            }).ToList();
        }

        public void AddLocationToCustomer(Guid customerId, LocationDTO locationDto)
        {
            var location = new Location
            {
                Address = locationDto.Address,
                City = locationDto.City,
                Country = locationDto.Country,
                PostalCode = locationDto.PostalCode,
                CustomerId = customerId
            };

            customerRepository.AddLocationToCustomer(customerId, location);
        }
    }
}
