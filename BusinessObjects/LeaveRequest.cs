using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class LeaveRequest
    {
        [Key]
        public Guid RequestId { get; set; } = Guid.NewGuid();
        
        public Guid? StaffId { get; set; }

        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        
        public DateTime? RequestDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? LeaveStart { get; set; }
        
        public DateTime? LeaveEnd { get; set; }
        
        public string? LeaveReasons { get; set; }
        
        public LeaveRequestStatus Status { get; set; } = LeaveRequestStatus.Pending;
    }
}
