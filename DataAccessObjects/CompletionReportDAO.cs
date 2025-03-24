using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class CompletionReportDAO
    {
        public static List<CompletionReport> GetCompletionReports()
        {
            var listReports = new List<CompletionReport>();
            try
            {
                using var db = new MyDbContext();
                listReports = db.CompletionReports
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                    .Include(r => r.ReportImage)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listReports;
        }

        public static void SaveCompletionReport(CompletionReport report)
        {
            try
            {
                using var context = new MyDbContext();
                context.CompletionReports.Add(report);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateCompletionReport(CompletionReport report)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<CompletionReport>(report).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteCompletionReport(CompletionReport report)
        {
            try
            {
                using var context = new MyDbContext();
                var reportToDelete = context.CompletionReports.SingleOrDefault(r => r.ReportId == report.ReportId);
                if (reportToDelete != null)
                {
                    context.CompletionReports.Remove(reportToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static CompletionReport GetCompletionReportById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.CompletionReports
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                    .Include(r => r.ReportImage)
                    .FirstOrDefault(r => r.ReportId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static CompletionReport GetCompletionReportByBookingId(Guid bookingId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.CompletionReports
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                    .Include(r => r.ReportImage)
                    .FirstOrDefault(r => r.BookingId.Equals(bookingId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<CompletionReport> GetCompletionReportsByStaffId(Guid staffId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.CompletionReports
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                    .Include(r => r.ReportImage)
                    .Where(r => r.Booking.StaffId == staffId)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<CompletionReport> GetCompletionReportsByCustomerId(Guid customerId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.CompletionReports
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                    .Include(r => r.ReportImage)
                    .Where(r => r.Booking.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<CompletionReport> GetCompletionReportsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using var db = new MyDbContext();
                return db.CompletionReports
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                    .Include(r => r.ReportImage)
                    .Where(r => r.ReportDateTime >= startDate && r.ReportDateTime <= endDate)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
