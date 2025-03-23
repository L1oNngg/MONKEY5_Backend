using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects.Helpers;

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
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTime BookingDate { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
