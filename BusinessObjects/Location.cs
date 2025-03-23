using System;
using System.ComponentModel.DataAnnotations;

namespace MONKEY5.BusinessObjects
{
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; } = Guid.NewGuid();

        [Required]
        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string DetailDescription { get; set; } = string.Empty;
    }
}
