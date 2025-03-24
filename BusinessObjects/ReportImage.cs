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
        public CompletionReport? CompletionReport { get; set; }

        [Required]
        public string? ImagePath { get; set; }
    }
}
