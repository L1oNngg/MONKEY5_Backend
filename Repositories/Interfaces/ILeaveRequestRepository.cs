using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface ILeaveRequestRepository
    {
        List<LeaveRequest> GetLeaveRequests();
        void SaveLeaveRequest(LeaveRequest request);
        void UpdateLeaveRequest(LeaveRequest request);
        void DeleteLeaveRequest(LeaveRequest request);
        LeaveRequest GetLeaveRequestById(Guid id);
        List<LeaveRequest> GetLeaveRequestsByStaffId(Guid staffId);
        List<LeaveRequest> GetLeaveRequestsByDateRange(DateTime startDate, DateTime endDate);
    }
}
