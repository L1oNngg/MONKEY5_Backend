using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public ServiceOrder? ServiceOrder { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public required decimal Amount { get; set; }

        [Required]
        [RegularExpression("Credit Card|PayPal|Bank Transfer|Cash")]
        public required string PaymentMethod { get; set; }

        [Required]
        [RegularExpression("Pending|Completed|Cancelled")]
        public required string PaymentStatus { get; set; }

        [StringLength(100)]
        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ConfirmedAt { get; set; }
    }
}
