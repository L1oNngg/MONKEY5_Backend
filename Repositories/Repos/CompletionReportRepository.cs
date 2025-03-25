using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class CompletionReportRepository : ICompletionReportRepository
    {
        public List<CompletionReport> GetCompletionReports() => 
            CompletionReportDAO.GetCompletionReports();
        
        public void SaveCompletionReport(CompletionReport report) => 
            CompletionReportDAO.SaveCompletionReport(report);
        
        public void UpdateCompletionReport(CompletionReport report) => 
            CompletionReportDAO.UpdateCompletionReport(report);
        
        public void DeleteCompletionReport(CompletionReport report) => 
            CompletionReportDAO.DeleteCompletionReport(report);
        
        public CompletionReport GetCompletionReportById(Guid id) => 
            CompletionReportDAO.GetCompletionReportById(id);
        
        public CompletionReport GetCompletionReportByBookingId(Guid bookingId) => 
            CompletionReportDAO.GetCompletionReportByBookingId(bookingId);
        
        public List<CompletionReport> GetCompletionReportsByStaffId(Guid staffId) => 
            CompletionReportDAO.GetCompletionReportsByStaffId(staffId);
        
        public List<CompletionReport> GetCompletionReportsByCustomerId(Guid customerId) => 
            CompletionReportDAO.GetCompletionReportsByCustomerId(customerId);
        
        public List<CompletionReport> GetCompletionReportsByDateRange(DateTime startDate, DateTime endDate) => 
            CompletionReportDAO.GetCompletionReportsByDateRange(startDate, endDate);
    }
}
