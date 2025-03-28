using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IStaffService
    {
        List<Staff> GetStaffs();
        void SaveStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(Staff staff);
        Staff? GetStaffById(Guid id);
        Staff? GetStaffByPhone(string phone);
        List<Staff> GetAvailableStaffs();
        Staff? Login(string email, string password); 
    }
}
