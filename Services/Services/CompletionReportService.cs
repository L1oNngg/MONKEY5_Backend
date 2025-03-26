using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class CompletionReportService : ICompletionReportService
    {
        private readonly ICompletionReportRepository completionReportRepository;

        public CompletionReportService()
        {
            completionReportRepository = new CompletionReportRepository();
        }

        public List<CompletionReport> GetCompletionReports() => 
            completionReportRepository.GetCompletionReports();
        
        public void SaveCompletionReport(CompletionReport report) => 
            completionReportRepository.SaveCompletionReport(report);
        
        public void UpdateCompletionReport(CompletionReport report) => 
            completionReportRepository.UpdateCompletionReport(report);
        
        public void DeleteCompletionReport(CompletionReport report) => 
            completionReportRepository.DeleteCompletionReport(report);
        
        public CompletionReport GetCompletionReportById(Guid id) => 
            completionReportRepository.GetCompletionReportById(id);
        
        public CompletionReport GetCompletionReportByBookingId(Guid bookingId) => 
            completionReportRepository.GetCompletionReportByBookingId(bookingId);
        
        public List<CompletionReport> GetCompletionReportsByStaffId(Guid staffId) => 
            completionReportRepository.GetCompletionReportsByStaffId(staffId);
        
        public List<CompletionReport> GetCompletionReportsByCustomerId(Guid customerId) => 
            completionReportRepository.GetCompletionReportsByCustomerId(customerId);
        
        public List<CompletionReport> GetCompletionReportsByDateRange(DateTime startDate, DateTime endDate) => 
            completionReportRepository.GetCompletionReportsByDateRange(startDate, endDate);
    }
}
