using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class BookingDAO
    {
        public static List<Booking> GetBookings()
        {
            var listBookings = new List<Booking>();
            try
            {
                using var db = new MyDbContext();
                listBookings = db.Bookings
                    .Include(b => b.Customer)
                        .ThenInclude(c => c.Location)
                    .Include(b => b.Staff)
                    .Include(b => b.Service)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listBookings;
        }

        public static void SaveBooking(Booking booking)
        {
            try
            {
                using var context = new MyDbContext();
                
                // Get the service to calculate the total price
                var service = context.Services.FirstOrDefault(s => s.ServiceId == booking.ServiceId);
                if (service != null)
                {
                    // Calculate total price based on service unit price and booking amount
                    booking.TotalPrice = (float)(service.UnitPrice * booking.ServiceUnitAmount);
                }
                
                context.Bookings.Add(booking);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateBooking(Booking booking)
        {
            try
            {
                using var context = new MyDbContext();
                
                // Get the existing booking from the database
                var existingBooking = context.Bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
                if (existingBooking == null)
                {
                    throw new Exception("Booking not found");
                }
                
                // Only update fields that are provided (not null or default)
                if (booking.CustomerId != Guid.Empty)
                    existingBooking.CustomerId = booking.CustomerId;
                    
                if (booking.StaffId != Guid.Empty)
                    existingBooking.StaffId = booking.StaffId;
                    
                if (booking.ServiceId != Guid.Empty)
                    existingBooking.ServiceId = booking.ServiceId;
                    
                // Enum values can be explicitly checked
                if (booking.Status != default)
                    existingBooking.Status = booking.Status;
                    
                // Never update BookingDateTime
                // existingBooking.BookingDateTime = booking.BookingDateTime;
                    
                if (booking.ServiceStartTime != default)
                    existingBooking.ServiceStartTime = booking.ServiceStartTime;
                    
                if (booking.ServiceEndTime != default)
                    existingBooking.ServiceEndTime = booking.ServiceEndTime;
                    
                // For numeric values, you might need to check if they're provided
                // This depends on your business logic
                if (booking.ServiceUnitAmount > 0)
                    existingBooking.ServiceUnitAmount = booking.ServiceUnitAmount;
                    
                // Update Note field if provided
                if (booking.Note != null)
                    existingBooking.Note = booking.Note;
                    
                // Recalculate total price if needed
                if (booking.ServiceId != Guid.Empty || booking.ServiceUnitAmount > 0)
                {
                    var service = context.Services.FirstOrDefault(s => s.ServiceId == existingBooking.ServiceId);
                    if (service != null)
                    {
                        existingBooking.TotalPrice = (float)(service.UnitPrice * existingBooking.ServiceUnitAmount);
                    }
                }
                else if (booking.TotalPrice > 0)
                {
                    existingBooking.TotalPrice = booking.TotalPrice;
                }
                
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static void DeleteBooking(Booking booking)
        {
            try
            {
                using var context = new MyDbContext();
                var bookingToDelete = context.Bookings.SingleOrDefault(b => b.BookingId == booking.BookingId);
                if (bookingToDelete != null)
                {
                    context.Bookings.Remove(bookingToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Booking GetBookingById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Bookings
                    .Include(b => b.Customer)
                        .ThenInclude(c => c.Location)
                    .Include(b => b.Staff)
                    .Include(b => b.Service)
                    .FirstOrDefault(b => b.BookingId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Booking> GetBookingsByCustomerId(Guid customerId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Bookings
                    .Include(b => b.Customer)
                        .ThenInclude(c => c.Location)
                    .Include(b => b.Staff)
                    .Include(b => b.Service)
                    .Where(b => b.CustomerId.Equals(customerId))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Booking> GetBookingsByStaffId(Guid staffId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Bookings
                    .Include(b => b.Customer)
                        .ThenInclude(c => c.Location)
                    .Include(b => b.Staff)
                    .Include(b => b.Service)
                    .Where(b => b.StaffId.Equals(staffId))
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Booking> GetBookingsByStatus(OrderStatus status)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Bookings
                    .Include(b => b.Customer)
                        .ThenInclude(c => c.Location)
                    .Include(b => b.Staff)
                    .Include(b => b.Service)
                    .Where(b => b.Status == status)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Bookings
                    .Include(b => b.Customer)
                        .ThenInclude(c => c.Location)
                    .Include(b => b.Staff)
                    .Include(b => b.Service)
                    .Where(b => b.ServiceStartTime >= startDate && b.ServiceEndTime <= endDate)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool IsStaffAvailable(Guid staffId, DateTime startTime, DateTime endTime)
        {
            try
            {
                using var db = new MyDbContext();
                // Check if staff has any overlapping bookings
                bool hasOverlappingBookings = db.Bookings
                    .Where(b => b.StaffId == staffId)
                    .Where(b => b.Status != OrderStatus.Cancelled && b.Status != OrderStatus.Completed)
                    .Any(b => 
                        (startTime >= b.ServiceStartTime && startTime < b.ServiceEndTime) || // Start time falls within existing booking
                        (endTime > b.ServiceStartTime && endTime <= b.ServiceEndTime) || // End time falls within existing booking
                        (startTime <= b.ServiceStartTime && endTime >= b.ServiceEndTime) // New booking completely encompasses existing booking
                    );
                
                return !hasOverlappingBookings;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
