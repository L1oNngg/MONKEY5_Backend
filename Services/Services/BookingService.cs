using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        public BookingService()
        {
            bookingRepository = new BookingRepository();
        }

        public List<Booking> GetBookings() => bookingRepository.GetBookings();
        
        public void SaveBooking(Booking booking) => bookingRepository.SaveBooking(booking);
        
        public void UpdateBooking(Booking booking) => bookingRepository.UpdateBooking(booking);
        
        public void DeleteBooking(Booking booking) => bookingRepository.DeleteBooking(booking);
        
        public Booking GetBookingById(Guid id) => bookingRepository.GetBookingById(id);
        
        public List<Booking> GetBookingsByCustomerId(Guid customerId) => 
            bookingRepository.GetBookingsByCustomerId(customerId);
        
        public List<Booking> GetBookingsByStaffId(Guid staffId) => 
            bookingRepository.GetBookingsByStaffId(staffId);
        
        public List<Booking> GetBookingsByStatus(OrderStatus status) => 
            bookingRepository.GetBookingsByStatus(status);
        
        public List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate) => 
            bookingRepository.GetBookingsByDateRange(startDate, endDate);
        
        public bool IsStaffAvailable(Guid staffId, DateTime startTime, DateTime endTime) => 
            bookingRepository.IsStaffAvailable(staffId, startTime, endTime);
    }
}
