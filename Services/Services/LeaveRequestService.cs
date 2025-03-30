using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public LeaveRequestService()
        {
            leaveRequestRepository = new LeaveRequestRepository();
        }

        public List<LeaveRequest> GetLeaveRequests() => leaveRequestRepository.GetLeaveRequests();
        
        public void SaveLeaveRequest(LeaveRequest request) => leaveRequestRepository.SaveLeaveRequest(request);
        
        public void UpdateLeaveRequest(LeaveRequest request) => leaveRequestRepository.UpdateLeaveRequest(request);
        
        public void DeleteLeaveRequest(LeaveRequest request) => leaveRequestRepository.DeleteLeaveRequest(request);
        
        public LeaveRequest GetLeaveRequestById(Guid id) => leaveRequestRepository.GetLeaveRequestById(id);
        
        public List<LeaveRequest> GetLeaveRequestsByStaffId(Guid staffId) => 
            leaveRequestRepository.GetLeaveRequestsByStaffId(staffId);
        
        public List<LeaveRequest> GetLeaveRequestsByDateRange(DateTime startDate, DateTime endDate) => 
            leaveRequestRepository.GetLeaveRequestsByDateRange(startDate, endDate);
    }
}
