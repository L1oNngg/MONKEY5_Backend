using System;
using System.ComponentModel.DataAnnotations;

namespace MONKEY5.BusinessObjects
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; } = Guid.NewGuid();

        [Required]
        public required string RoleName { get; set; }
    }
}
