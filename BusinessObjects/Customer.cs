using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Customer: User
    {
        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
