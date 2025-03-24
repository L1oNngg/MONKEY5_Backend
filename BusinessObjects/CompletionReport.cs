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

        [Required]
        public List<string> ReportImageId { get; set; } = [];

        [ForeignKey("ReportImageId")]
        public ReportImage? ReportImage { get; set; }

        public DateTime ReportDateTime { get; set; } = DateTime.UtcNow;
    }
}
