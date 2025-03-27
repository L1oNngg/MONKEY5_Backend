using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid? BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public required decimal Amount { get; set; }

        [Required]
        public required PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public DateTime PaymentCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaymentPaidAt { get; set; }
    }
}
