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
    }
}
