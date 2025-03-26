using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTime BookingDateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ServiceStartTime { get; set; }

        [Required]
        public DateTime ServiceEndTime { get; set; }

        [Required]
        public int ServiceUnitAmount { get; set; }

        [Required]
        public float TotalPrice { get; set; }
    }
}
