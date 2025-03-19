using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Employee : User
    {
        [Required]
        public Guid StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [Range(0, 5)]
        public double? Rating { get; set; }
    }
}
