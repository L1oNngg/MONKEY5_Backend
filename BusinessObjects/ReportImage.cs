using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class ReportImage
    {
        [Key]
        public Guid ReportImageId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ReportId { get; set; }

        [ForeignKey("ReportId")]
        public Report? Report { get; set; }

        [Required]
        public string? ImagePath { get; set; }
    }
}
