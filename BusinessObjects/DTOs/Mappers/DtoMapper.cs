using System.Collections.Generic;
using System.Linq;

namespace MONKEY5.BusinessObjects.DTOs.Mappers
{
    public static class DtoMapper
    {
        public static LocationDTO ToLocationDto(this Location location)
        {
            if (location == null) return null;
            
            return new LocationDTO
            {
                LocationId = location.LocationId,
                Address = location.Address,
                City = location.City,
                Country = location.Country,
                PostalCode = location.PostalCode,
                CustomerId = location.CustomerId
            };
        }
        
        public static CustomerDTO ToCustomerDto(this Customer customer)
        {
            if (customer == null) return null;
            
            return new CustomerDTO
            {
                UserId = customer.UserId,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                IdNumber = customer.IdNumber,
                Role = customer.Role.ToString(),
                Locations = customer.Locations?.Select(l => l.ToLocationDto()).ToList(),
                RegistrationDate = customer.RegistrationDate
            };
        }
        
        public static StaffDTO ToStaffDto(this Staff staff)
        {
            if (staff == null) return null;
            
            return new StaffDTO
            {
                UserId = staff.UserId,
                FullName = staff.FullName,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                DateOfBirth = staff.DateOfBirth,
                Gender = staff.Gender,
                IdNumber = staff.IdNumber,
                Role = staff.Role.ToString(),
                AvgRating = staff.AvgRating,
                Status = staff.Status
            };
        }
        
        public static ServiceDTO ToServiceDto(this Service service)
        {
            if (service == null) return null;
            
            return new ServiceDTO
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Description = service.Description,
                UnitPrice = service.UnitPrice,
                UnitType = service.UnitType
            };
        }
        
        public static BookingDTO ToBookingDto(this Booking booking)
        {
            if (booking == null) return null;
            
            return new BookingDTO
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                Customer = booking.Customer?.ToCustomerDto(),
                StaffId = booking.StaffId,
                Staff = booking.Staff?.ToStaffDto(),
                ServiceId = booking.ServiceId,
                Service = booking.Service?.ToServiceDto(),
                Status = booking.Status,
                BookingDateTime = booking.BookingDateTime,
                ServiceStartTime = booking.ServiceStartTime,
                ServiceEndTime = booking.ServiceEndTime,
                ServiceUnitAmount = booking.ServiceUnitAmount,
                TotalPrice = booking.TotalPrice,
                Note = booking.Note,
                LocationId = booking.LocationId,
                Location = booking.Location?.ToLocationDto()
            };
        }
        
        public static List<BookingDTO> ToBookingDtoList(this IEnumerable<Booking> bookings)
        {
            return bookings?.Select(b => b.ToBookingDto()).ToList() ?? new List<BookingDTO>();
        }
    }
}
