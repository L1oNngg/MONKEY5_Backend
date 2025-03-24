using System;
using System.ComponentModel.DataAnnotations;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class Staff: User
    {
        [Range(1, 5)]
        public double AvgRating { get; set; } = 0;

        [Required]
        public AvailabilityStatus Status { get; set; } = AvailabilityStatus.Available;
    }
}
