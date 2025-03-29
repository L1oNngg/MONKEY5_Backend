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
                    .Include(r => r.ReportImages)
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
                
                // Handle the case where report images are included
                var reportImages = report.ReportImages?.ToList();
                report.ReportImages = null; // Temporarily remove to avoid relationship conflicts
                
                // Add and save the report first
                context.CompletionReports.Add(report);
                context.SaveChanges();
                
                // Now handle the images if any
                if (reportImages != null && reportImages.Any())
                {
                    foreach (var image in reportImages)
                    {
                        // Set the correct ReportId
                        image.ReportId = report.ReportId;
                        
                        // If it's a new image (no ID or default ID), add it
                        if (image.ReportImageId == Guid.Empty || image.ReportImageId == default)
                        {
                            image.ReportImageId = Guid.NewGuid();
                            context.ReportImages.Add(image);
                        }
                        // If it's an existing image, attach and update it
                        else
                        {
                            context.Entry(image).State = EntityState.Modified;
                        }
                    }
                    
                    context.SaveChanges();
                }
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
                    .Include(r => r.ReportImages)
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
                    .Include(r => r.ReportImages)
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
                    .Include(r => r.ReportImages)
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
                    .Include(r => r.ReportImages)
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
                    .Include(r => r.ReportImages)
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
