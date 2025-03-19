using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class ServiceOption
    {
        [Key]
        public Guid OptionId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

        [Required]
        public required string OptionName { get; set; }

        [Required]
        public required string Unit { get; set; }

        [Required]
        public decimal PricePerUnit { get; set; }

        public string? Conditions { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
