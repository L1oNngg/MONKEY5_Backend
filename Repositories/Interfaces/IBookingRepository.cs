using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookingRepository
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
    }
}
