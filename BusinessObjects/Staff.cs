using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class Staff: User
    {
        [Range(0, 5)]
        public double AvgRating { get; set; } = 0;
        public AvailabilityStatus Status { get; set; } = AvailabilityStatus.Available;
        
        [NotMapped] // This ensures it's not stored in the database
        public int BookingCount { get; set; } = 0;
    }
}
