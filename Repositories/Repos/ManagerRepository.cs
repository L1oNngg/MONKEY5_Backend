using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        public List<Manager> GetManagers() => ManagerDAO.GetManagers();
        
        public void SaveManager(Manager manager) => ManagerDAO.SaveManager(manager);
        
        public void UpdateManager(Manager manager) => ManagerDAO.UpdateManager(manager);
        
        public void DeleteManager(Manager manager) => ManagerDAO.DeleteManager(manager);
        
        public Manager GetManagerById(Guid id) => ManagerDAO.GetManagerById(id);
        
        public Manager GetManagerByEmail(string email) => ManagerDAO.GetManagerByEmail(email);

        public Manager? Login(string email, string password) => ManagerDAO.Login(email, password);
    }
}
