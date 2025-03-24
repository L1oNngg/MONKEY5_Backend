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

        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng. Vui lòng nhập lại!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
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

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        [RegularExpression("Male|Female|Other")]
        public string? Gender { get; set; }

        public string? Address { get; set; }
        public string? IDNumber { get; set; }

        [Required]
        public Role Role { get; set; } = Role.Customer;
    }
}
