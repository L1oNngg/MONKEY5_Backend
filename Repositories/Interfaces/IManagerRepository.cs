using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface IManagerRepository
    {
        List<Manager> GetManagers();
        void SaveManager(Manager manager);
        void UpdateManager(Manager manager);
        void DeleteManager(Manager manager);
        Manager GetManagerById(Guid id);
        Manager GetManagerByEmail(string email);
    }
}
