using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public List<User> GetUsers() => userRepository.GetUsers();
        
        public void SaveUser(User user) => userRepository.SaveUser(user);
        
        public void UpdateUser(User user) => userRepository.UpdateUser(user);
        
        public void DeleteUser(User user) => userRepository.DeleteUser(user);
        
        public User? GetUserById(Guid id) => userRepository.GetUserById(id);
        
        public User? GetUserByPhone(string phoneNumber) => userRepository.GetUserByPhone(phoneNumber);
    }
}
