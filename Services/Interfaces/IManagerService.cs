using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IManagerService
    {
        List<Manager> GetManagers();
        void SaveManager(Manager manager);
        void UpdateManager(Manager manager);
        void DeleteManager(Manager manager);
        Manager GetManagerById(Guid id);
        Manager GetManagerByEmail(string email);

        Manager? Login(string email, string password);
    }
}
