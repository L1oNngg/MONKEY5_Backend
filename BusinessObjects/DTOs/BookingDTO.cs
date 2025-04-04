using System;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects.DTOs
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }
        public Guid? CustomerId { get; set; }
        public CustomerDTO? Customer { get; set; }
        public Guid? StaffId { get; set; }
        public StaffDTO? Staff { get; set; }
        public Guid? ServiceId { get; set; }
        public ServiceDTO? Service { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? BookingDateTime { get; set; }
        public DateTime? ServiceStartTime { get; set; }
        public DateTime? ServiceEndTime { get; set; }
        public int? ServiceUnitAmount { get; set; }
        public float? TotalPrice { get; set; }
        public string? Note { get; set; }
        public Guid? LocationId { get; set; }
        public LocationDTO? Location { get; set; }
    }
}
