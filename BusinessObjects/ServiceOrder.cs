using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class ServiceOrder
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [Required]
        public Guid OptionId { get; set; }

        [ForeignKey("OptionId")]
        public ServiceOption? ServiceOption { get; set; }

        [Required]
        public Guid StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public DateTime BookingDate { get; set; }

        [Required]
        [RegularExpression("Pending|Completed|Cancelled")]
        public string? PaymentStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
