using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; } = Guid.NewGuid();
        public Guid? BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        [Range(1, 5)]
        public int? RatingStar { get; set; }

        public string? Comment { get; set; }

        public DateTime ReviewDateTime { get; set; } = DateTime.UtcNow;
    }
}
