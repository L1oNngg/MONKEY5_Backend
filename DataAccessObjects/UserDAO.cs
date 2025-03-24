using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class UserDAO
    {
        public static List<User> GetUsers()
        {
            var listUsers = new List<User>();
            try
            {
                using var db = new MyDbContext();
                listUsers = db.Users.ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listUsers;
        }

        public static void SaveUser(User user)
        {
            try
            {
                using var context = new MyDbContext();
                // Hash the password before saving
                user.HashPassword();
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateUser(User user)
        {
            try
            {
                using var context = new MyDbContext();
                // Check if password needs to be hashed (if it was changed)
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.HashPassword();
                }
                context.Entry<User>(user).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteUser(User user)
        {
            try
            {
                using var context = new MyDbContext();
                var userToDelete = context.Users.SingleOrDefault(u => u.UserId == user.UserId);
                if (userToDelete != null)
                {
                    context.Users.Remove(userToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User GetUserById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Users.FirstOrDefault(u => u.UserId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static User GetUserByEmail(string email)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Users.FirstOrDefault(u => u.Email.Equals(email));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
