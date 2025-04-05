using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingService
    {
        List<Booking> GetBookings();
        void SaveBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
        Booking GetBookingById(Guid id);
        List<Booking> GetBookingsByCustomerId(Guid customerId);
        List<Booking> GetBookingsByStaffId(Guid staffId);
        List<Booking> GetBookingsByStatus(OrderStatus status);
        List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
        bool IsStaffAvailable(Guid staffId, DateTime startTime, DateTime endTime);
        Staff AssignStaffToBooking(Guid bookingId);
        bool AcceptBookingAssignment(Guid bookingId, Guid staffId);
        Staff ReassignBookingToNextAvailableStaff(Guid bookingId, Guid declinedStaffId);

    }
}
