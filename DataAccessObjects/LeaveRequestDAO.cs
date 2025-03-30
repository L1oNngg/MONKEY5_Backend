using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class LeaveRequestDAO
    {
        public static List<LeaveRequest> GetLeaveRequests()
        {
            var listRequests = new List<LeaveRequest>();
            try
            {
                using var db = new MyDbContext();
                listRequests = db.LeaveRequests
                    .Include(l => l.Staff)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listRequests;
        }

        public static void SaveLeaveRequest(LeaveRequest request)
        {
            try
            {
                using var context = new MyDbContext();
                context.LeaveRequests.Add(request);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateLeaveRequest(LeaveRequest request)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<LeaveRequest>(request).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteLeaveRequest(LeaveRequest request)
        {
            try
            {
                using var context = new MyDbContext();
                var requestToDelete = context.LeaveRequests.SingleOrDefault(r => r.RequestId == request.RequestId);
                if (requestToDelete != null)
                {
                    context.LeaveRequests.Remove(requestToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static LeaveRequest GetLeaveRequestById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.LeaveRequests
                    .Include(l => l.Staff)
                    .FirstOrDefault(l => l.RequestId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<LeaveRequest> GetLeaveRequestsByStaffId(Guid staffId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.LeaveRequests
                    .Include(l => l.Staff)
                    .Where(l => l.StaffId.Equals(staffId))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<LeaveRequest> GetLeaveRequestsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using var db = new MyDbContext();
                return db.LeaveRequests
                    .Include(l => l.Staff)
                    .Where(l => 
                        (l.LeaveStart >= startDate && l.LeaveStart <= endDate) || 
                        (l.LeaveEnd >= startDate && l.LeaveEnd <= endDate) ||
                        (l.LeaveStart <= startDate && l.LeaveEnd >= endDate))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
