using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        public static List<Customer> GetCustomers()
        {
            var listCustomers = new List<Customer>();
            try
            {
                using var db = new MyDbContext();
                listCustomers = db.Customers.Include(c => c.Locations).ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listCustomers;
        }

        public static void SaveCustomer(Customer customer)
        {
            try
            {
                using var context = new MyDbContext();
                // Hash the password before saving
                customer.HashPassword();
                // Set role to Customer
                customer.Role = MONKEY5.BusinessObjects.Helpers.Role.Customer;
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new MyDbContext();
                // Check if password needs to be hashed (if it was changed)
                if (!string.IsNullOrEmpty(customer.Password))
                {
                    customer.HashPassword();
                }
                context.Entry<Customer>(customer).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteCustomer(Customer customer)
        {
            try
            {
                using var context = new MyDbContext();
                var customerToDelete = context.Customers.SingleOrDefault(c => c.UserId == customer.UserId);
                if (customerToDelete != null)
                {
                    context.Customers.Remove(customerToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Customer GetCustomerById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Customers.Include(c => c.Locations).FirstOrDefault(c => c.UserId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Customer GetCustomerByEmail(string email)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Customers.Include(c => c.Locations).FirstOrDefault(c => c.Email.Equals(email));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Customer? Login(string email, string password)
        {
            try
            {
                using var db = new MyDbContext();
                var customer = db.Customers.Include(c => c.Locations).FirstOrDefault(c => c.Email.Equals(email));
                
                if (customer != null && !string.IsNullOrEmpty(customer.PasswordHash))
                {
                    bool isPasswordValid = PasswordHasher.VerifyPassword(password, customer.PasswordHash);
                    if (isPasswordValid)
                    {
                        return customer;
                    }
                }
                
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
