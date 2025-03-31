using System;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects.DTOs
{
    public class StaffDTO
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? IdNumber { get; set; }
        public string? Role { get; set; }
        public double AvgRating { get; set; }
        public AvailabilityStatus Status { get; set; }
    }
}
