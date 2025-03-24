using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Manager: User
    {
        [Key]
        public Guid ManagerId { get; set; } = Guid.NewGuid();

        [ForeignKey("UserId")]
        public User? UserId { get; set; }
    }
}
