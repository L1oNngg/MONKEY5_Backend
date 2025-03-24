using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; } = Guid.NewGuid();

        [Required]
        public required string ServiceName { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        public required string UnitType { get; set; }
    }
}
