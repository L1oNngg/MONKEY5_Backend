using MONKEY5.BusinessObjects;
using BusinessObjects.Helpers;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        public List<LeaveRequest> GetLeaveRequests() => LeaveRequestDAO.GetLeaveRequests();
        
        public void SaveLeaveRequest(LeaveRequest request) => LeaveRequestDAO.SaveLeaveRequest(request);
        
        public void UpdateLeaveRequest(LeaveRequest request) => LeaveRequestDAO.UpdateLeaveRequest(request);
        
        public void DeleteLeaveRequest(LeaveRequest request) => LeaveRequestDAO.DeleteLeaveRequest(request);
        
        public LeaveRequest GetLeaveRequestById(Guid id) => LeaveRequestDAO.GetLeaveRequestById(id);
        
        public List<LeaveRequest> GetLeaveRequestsByStaffId(Guid staffId) => 
            LeaveRequestDAO.GetLeaveRequestsByStaffId(staffId);
        
        public List<LeaveRequest> GetLeaveRequestsByDateRange(DateTime startDate, DateTime endDate) => 
            LeaveRequestDAO.GetLeaveRequestsByDateRange(startDate, endDate);
    }
}
