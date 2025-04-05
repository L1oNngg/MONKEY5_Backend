using System;

namespace MONKEY5.BusinessObjects.DTOs
{
    public class BookingAssignmentResponseDTO
    {
        public Guid BookingId { get; set; }
        public Guid StaffId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
