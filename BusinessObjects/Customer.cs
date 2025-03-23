using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Customer: User
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location? Location { get; set; }

        [Range(0, int.MaxValue)]
        public int RewardPoints { get; set; } = 0;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}

