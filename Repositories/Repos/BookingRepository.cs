using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public List<Booking> GetBookings() => BookingDAO.GetBookings();
        
        public void SaveBooking(Booking booking) => BookingDAO.SaveBooking(booking);
        
        public void UpdateBooking(Booking booking) => BookingDAO.UpdateBooking(booking);
        
        public void DeleteBooking(Booking booking) => BookingDAO.DeleteBooking(booking);
        
        public Booking GetBookingById(Guid id) => BookingDAO.GetBookingById(id);
        
        public List<Booking> GetBookingsByCustomerId(Guid customerId) => 
            BookingDAO.GetBookingsByCustomerId(customerId);
        
        public List<Booking> GetBookingsByStaffId(Guid staffId) => 
            BookingDAO.GetBookingsByStaffId(staffId);
        
        public List<Booking> GetBookingsByStatus(OrderStatus status) => 
            BookingDAO.GetBookingsByStatus(status);
        
        public List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate) => 
            BookingDAO.GetBookingsByDateRange(startDate, endDate);
        
        public bool IsStaffAvailable(Guid staffId, DateTime startTime, DateTime endTime) => 
            BookingDAO.IsStaffAvailable(staffId, startTime, endTime);

        public Staff AssignStaffToBooking(Guid bookingId)
        {
            // Get the booking
            var booking = BookingDAO.GetBookingById(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");
                
            // Get the current month's date range - MAKE SURE TO USE UTC
            var today = DateTime.UtcNow;
            var monthStart = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToUniversalTime();
            
            // Get available staff with booking counts
            var availableStaffs = StaffDAO.GetAvailableStaffsWithBookingCounts(monthStart, monthEnd);
            
            if (!availableStaffs.Any())
                throw new Exception("No available staff found");
                
            // Get the staff with the minimum booking count
            var selectedStaff = availableStaffs.First();
            
            // Update the booking with the selected staff
            booking.StaffId = selectedStaff.UserId;
            booking.Status = OrderStatus.Assigned;
            BookingDAO.UpdateBooking(booking);
            
            return selectedStaff;
        }

        public bool AcceptBookingAssignment(Guid bookingId, Guid staffId)
        {
            var booking = BookingDAO.GetBookingById(bookingId);
            if (booking == null || booking.StaffId != staffId)
                return false;
                
            booking.Status = OrderStatus.Confirmed;
            BookingDAO.UpdateBooking(booking);
            return true;
        }

        public Staff ReassignBookingToNextAvailableStaff(Guid bookingId, Guid declinedStaffId)
        {
            var booking = BookingDAO.GetBookingById(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");
                
            // Get the current month's date range - MAKE SURE TO USE UTC
            var today = DateTime.UtcNow;
            var monthStart = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1).Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToUniversalTime();
            
            // Get available staff with booking counts
            var availableStaffs = StaffDAO.GetAvailableStaffsWithBookingCounts(monthStart, monthEnd)
                .Where(s => s.UserId != declinedStaffId)
                .ToList();
            
            if (!availableStaffs.Any())
            {
                // No more available staff, cancel the booking
                booking.Status = OrderStatus.Cancelled;
                booking.StaffId = null;
                BookingDAO.UpdateBooking(booking);
                return null;
            }
            
            // Get the staff with the minimum booking count
            var selectedStaff = availableStaffs.First();
            
            // Update the booking with the selected staff
            booking.StaffId = selectedStaff.UserId;
            booking.Status = OrderStatus.Assigned;
            BookingDAO.UpdateBooking(booking);
            
            return selectedStaff;
        }

    }
}
