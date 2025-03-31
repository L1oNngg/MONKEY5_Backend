using System;

namespace MONKEY5.BusinessObjects.DTOs
{
    public class LocationDTO
    {
        public Guid LocationId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
