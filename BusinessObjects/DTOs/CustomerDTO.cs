using System;

namespace MONKEY5.BusinessObjects.DTOs
{
    public class CustomerDTO
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? IdNumber { get; set; }
        public string? Role { get; set; }
        public Guid? LocationId { get; set; }
        public LocationDTO? Location { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
