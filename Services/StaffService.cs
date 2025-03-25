using MONKEY5.BusinessObjects;
using MONKEY5.Repositories;

namespace MONKEY5.Services
{
    public class StaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IEnumerable<Staff>> GetAllStaffsAsync()
        {
            return await _staffRepository.GetAllAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(Guid id)
        {
            return await _staffRepository.GetStaffByIdAsync(id);
        }

        public async Task<Staff?> GetStaffByPhoneAsync(string phone)
        {
            return await _staffRepository.GetStaffByPhoneAsync(phone);
        }

        public async Task<IEnumerable<Staff>> GetAvailableStaffsAsync()
        {
            return await _staffRepository.GetAvailableStaffsAsync();
        }

        public async Task AddStaffAsync(Staff staff)
        {
            // Hash the password before saving
            staff.HashPassword();
            staff.Role = MONKEY5.BusinessObjects.Helpers.Role.Staff;

            await _staffRepository.AddAsync(staff);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task UpdateStaffAsync(Staff staff)
        {
            // Hash the password if it was changed
            if (!string.IsNullOrEmpty(staff.Password))
            {
                staff.HashPassword();
            }

            _staffRepository.Update(staff);
            await _staffRepository.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepository.GetStaffByIdAsync(id);
            if (staff != null)
            {
                _staffRepository.Delete(staff);
                await _staffRepository.SaveChangesAsync();
            }
        }
    }
}
