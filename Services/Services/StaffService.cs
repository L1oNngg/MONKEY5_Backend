using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository staffRepository;

        public StaffService()
        {
            staffRepository = new StaffRepository();
        }

        public List<Staff> GetStaffs() => staffRepository.GetStaffs();
        
        public void SaveStaff(Staff staff) => staffRepository.SaveStaff(staff);
        
        public void UpdateStaff(Staff staff) => staffRepository.UpdateStaff(staff);
        
        public void DeleteStaff(Staff staff) => staffRepository.DeleteStaff(staff);
        
        public Staff? GetStaffById(Guid id) => staffRepository.GetStaffById(id);
        
        public Staff? GetStaffByPhone(string phone) => staffRepository.GetStaffByPhone(phone);
        
        public List<Staff> GetAvailableStaffs() => staffRepository.GetAvailableStaffs();
    }
}
