using MONKEY5.BusinessObjects;
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
    }
}
