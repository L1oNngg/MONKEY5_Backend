using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        void SaveUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User? GetUserById(Guid id);
        User? GetUserByPhone(string phoneNumber);
    }
}
