using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetUsers() => UserDAO.GetUsers();
        
        public void SaveUser(User user) => UserDAO.SaveUser(user);
        
        public void UpdateUser(User user) => UserDAO.UpdateUser(user);
        
        public void DeleteUser(User user) => UserDAO.DeleteUser(user);
        
        public User? GetUserById(Guid id) => UserDAO.GetUserById(id);
        
        public User? GetUserByPhone(string phoneNumber) => UserDAO.GetUserByPhone(phoneNumber);
    }
}
