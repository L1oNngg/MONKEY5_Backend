using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository managerRepository;

        public ManagerService()
        {
            managerRepository = new ManagerRepository();
        }

        public List<Manager> GetManagers() => managerRepository.GetManagers();
        
        public void SaveManager(Manager manager) => managerRepository.SaveManager(manager);
        
        public void UpdateManager(Manager manager) => managerRepository.UpdateManager(manager);
        
        public void DeleteManager(Manager manager) => managerRepository.DeleteManager(manager);
        
        public Manager GetManagerById(Guid id) => managerRepository.GetManagerById(id);
        
        public Manager GetManagerByEmail(string email) => managerRepository.GetManagerByEmail(email);

        public Manager? Login(string email, string password) => managerRepository.Login(email, password);
    }
}
