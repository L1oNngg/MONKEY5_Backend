using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class CompletionReport
    {
        [Key]
        public Guid ReportId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        [Required]
        public string ReportText { get; set; } = "";

        // Remove the ReportImageId property and ForeignKey attribute
        // Instead, use a navigation property for the collection
        public virtual ICollection<ReportImage> ReportImages { get; set; } = new List<ReportImage>();

        public DateTime ReportDateTime { get; set; } = DateTime.UtcNow;
    }
}
