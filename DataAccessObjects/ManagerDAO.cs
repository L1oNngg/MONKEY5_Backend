using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class ManagerDAO
    {
        public static List<Manager> GetManagers()
        {
            var listManagers = new List<Manager>();
            try
            {
                using var db = new MyDbContext();
                listManagers = db.Managers.ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listManagers;
        }

        public static void SaveManager(Manager manager)
        {
            try
            {
                using var context = new MyDbContext();
                // Hash the password before saving
                manager.HashPassword();
                // Set role to Manager
                manager.Role = MONKEY5.BusinessObjects.Helpers.Role.Manager;
                context.Managers.Add(manager);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    public static void UpdateManager(Manager manager)
    {
        try
        {
            using var context = new MyDbContext();
            
            // Get the existing manager from the database
            var existingManager = context.Managers.FirstOrDefault(m => m.UserId == manager.UserId);
            if (existingManager == null)
            {
                throw new Exception("Manager not found");
            }
            
            // Update properties
            existingManager.FullName = manager.FullName;
            existingManager.Email = manager.Email;
            existingManager.PhoneNumber = manager.PhoneNumber;
            existingManager.DateOfBirth = manager.DateOfBirth;
            existingManager.Gender = manager.Gender;
            existingManager.IdNumber = manager.IdNumber;
            
            // Always ensure the role is Manager
            existingManager.Role = MONKEY5.BusinessObjects.Helpers.Role.Manager;
            
            // Check if password needs to be hashed (if it was changed)
            if (!string.IsNullOrEmpty(manager.Password))
            {
                existingManager.Password = manager.Password;
                existingManager.HashPassword();
            }
            
            context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

        public static void DeleteManager(Manager manager)
        {
            try
            {
                using var context = new MyDbContext();
                var managerToDelete = context.Managers.SingleOrDefault(m => m.UserId == manager.UserId);
                if (managerToDelete != null)
                {
                    context.Managers.Remove(managerToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Manager GetManagerById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Managers.FirstOrDefault(m => m.UserId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Manager GetManagerByEmail(string email)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Managers.FirstOrDefault(m => m.Email.Equals(email));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Manager? Login(string email, string password)
        {
            try
            {
                using var db = new MyDbContext();
                var manager = db.Managers.FirstOrDefault(m => m.Email.Equals(email));
                
                if (manager != null && !string.IsNullOrEmpty(manager.PasswordHash))
                {
                    bool isPasswordValid = PasswordHasher.VerifyPassword(password, manager.PasswordHash);
                    if (isPasswordValid)
                    {
                        return manager;
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
