using MONKEY5.BusinessObjects;

namespace MONKEY5.Repositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff?> GetStaffByIdAsync(Guid id);
        Task<Staff?> GetStaffByPhoneAsync(string phone);
        Task<IEnumerable<Staff>> GetAvailableStaffsAsync();
    }
}
