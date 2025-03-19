using System;
using System.ComponentModel.DataAnnotations;

namespace MONKEY5.BusinessObjects
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; } = Guid.NewGuid();

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
