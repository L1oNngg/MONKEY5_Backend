using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();
        
        public void SaveCustomer(Customer customer) => CustomerDAO.SaveCustomer(customer);
        
        public void UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);
        
        public void DeleteCustomer(Customer customer) => CustomerDAO.DeleteCustomer(customer);
        
        public Customer GetCustomerById(Guid id) => CustomerDAO.GetCustomerById(id);
        
        public Customer GetCustomerByEmail(string email) => CustomerDAO.GetCustomerByEmail(email);

        public Customer? Login(string email, string password) => CustomerDAO.Login(email, password);

        public ICollection<Location> GetCustomerLocations(Guid customerId)
        {
            var customer = CustomerDAO.GetCustomerById(customerId);
            return customer?.Locations ?? new List<Location>();
        }

        public void AddLocationToCustomer(Guid customerId, Location location)
        {
            var customer = CustomerDAO.GetCustomerById(customerId);
            if (customer != null)
            {
                customer.Locations.Add(location);
                CustomerDAO.UpdateCustomer(customer);
            }
        }
    }
}
