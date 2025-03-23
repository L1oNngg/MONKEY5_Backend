using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Staff: User
    {
        [Key]
        public Guid StaffId { get; set; } = Guid.NewGuid();

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;
    }
}
