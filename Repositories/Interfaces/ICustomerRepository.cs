using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        void SaveCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Customer GetCustomerById(Guid id);
        Customer GetCustomerByEmail(string email);
    }
}
