using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
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
                // Check if password needs to be hashed (if it was changed)
                if (!string.IsNullOrEmpty(manager.Password))
                {
                    manager.HashPassword();
                }
                context.Entry<Manager>(manager).State = EntityState.Modified;
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
    }
}
