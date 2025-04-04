using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects.Helpers;
using MONKEY5.BusinessObjects.DTOs;

namespace MONKEY5.BusinessObjects
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; } = Guid.NewGuid();
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public Guid? StaffId { get; set; }

        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        public Guid? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTime BookingDateTime { get; set; } = DateTime.UtcNow;
        public DateTime ServiceStartTime { get; set; }
        public DateTime ServiceEndTime { get; set; }
        public int? ServiceUnitAmount { get; set; }
        public float? TotalPrice { get; set; }
        public string? Note { get; set; }

        [ForeignKey("LocationId")]
        public Guid? LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
