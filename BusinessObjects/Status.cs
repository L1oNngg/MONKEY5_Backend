using System;
using System.ComponentModel.DataAnnotations;

namespace MONKEY5.BusinessObjects
{
    public class Status
    {
        [Key]
        public Guid StatusId { get; set; } = Guid.NewGuid();

        [Required]
        public required string StatusName { get; set; }
    }
}
