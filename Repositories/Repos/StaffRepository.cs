using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class StaffRepository : IStaffRepository
    {
        public List<Staff> GetStaffs() => StaffDAO.GetStaffs();
        
        public void SaveStaff(Staff staff) => StaffDAO.SaveStaff(staff);
        
        public void UpdateStaff(Staff staff) => StaffDAO.UpdateStaff(staff);
        
        public void DeleteStaff(Staff staff) => StaffDAO.DeleteStaff(staff);
        
        public Staff? GetStaffById(Guid id) => StaffDAO.GetStaffById(id);
        
        public Staff? GetStaffByPhone(string phone) => StaffDAO.GetStaffByPhone(phone);
        
        public List<Staff> GetAvailableStaffs() => StaffDAO.GetAvailableStaffs();

        public Staff? Login(string email, string password) => StaffDAO.Login(email, password);
    }
}
