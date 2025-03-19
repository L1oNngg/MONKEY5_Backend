using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class TaskAssignment
    {
        [Key]
        public Guid AssignmentId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public ServiceOrder? ServiceOrder { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [ForeignKey("StaffId")]
        public Employee? Employee { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public string? ProgressReport { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
