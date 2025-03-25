using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface ICompletionReportRepository
    {
        List<CompletionReport> GetCompletionReports();
        void SaveCompletionReport(CompletionReport report);
        void UpdateCompletionReport(CompletionReport report);
        void DeleteCompletionReport(CompletionReport report);
        CompletionReport GetCompletionReportById(Guid id);
        CompletionReport GetCompletionReportByBookingId(Guid bookingId);
        List<CompletionReport> GetCompletionReportsByStaffId(Guid staffId);
        List<CompletionReport> GetCompletionReportsByCustomerId(Guid customerId);
        List<CompletionReport> GetCompletionReportsByDateRange(DateTime startDate, DateTime endDate);
    }
}
