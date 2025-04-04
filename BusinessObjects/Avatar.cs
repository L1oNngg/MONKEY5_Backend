using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects;

namespace BusinessObjects
{
    public class Avatar
    {
        [Key]
        public Guid AvatarId { get; set; }
        
        [ForeignKey("Customer")]
        public Guid? UserId { get; set; }
        
        public string? AvatarImagePath { get; set; }
        
        public virtual Customer? Customer { get; set; }
    }
}
