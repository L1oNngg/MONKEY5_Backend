using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class Service
    {
        [Key]
        public Guid ServiceId { get; set; } = Guid.NewGuid();
        public string? ServiceName { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? UnitPrice { get; set; }

        public string? UnitType { get; set; }

        // Add status property for service
        public ServiceStatus Status { get; set; } = ServiceStatus.Available;
    }
}
