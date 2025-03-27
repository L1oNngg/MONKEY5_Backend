using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MONKEY5.BusinessObjects.Helpers;

namespace MONKEY5.BusinessObjects
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? FullName { get; set; }

        // [EmailAddress(ErrorMessage = "Email không đúng định dạng. Vui lòng nhập lại!")]
        public string? Email { get; set; }

        [NotMapped] // Exclude from database
        public string? Password { get; set; } 

        // Store the hashed password
        public string? PasswordHash { get; set; }

        public void HashPassword()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                PasswordHash = PasswordHasher.HashPassword(Password);
                Password = null; // Clear plain text password
            }
        }

        // [Phone]
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public string? IdNumber { get; set; }
        public Role Role { get; set; } = Role.Customer;
    }
}
