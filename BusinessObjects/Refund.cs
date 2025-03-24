using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Refund
    {
        [Key]
        public Guid RefundId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid PaymentId { get; set; }

        [ForeignKey("PaymentId")]
        public virtual Payment? Payment { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal RefundAmount { get; set; }

        public string? RefundReason { get; set; }

        [Required]
        public DateTime RefundDateTime { get; set; } = DateTime.UtcNow;
    }
}
