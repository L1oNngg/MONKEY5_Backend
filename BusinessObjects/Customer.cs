using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Customer: User
    {
        public Guid LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location? Location { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}

