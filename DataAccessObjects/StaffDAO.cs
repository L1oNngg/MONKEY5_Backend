using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class StaffDAO
    {
        public static List<Staff> GetStaffs()
        {
            var listStaffs = new List<Staff>();
            try
            {
                using var db = new MyDbContext();
                listStaffs = db.Staffs.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listStaffs;
        }

        public static void SaveStaff(Staff staff)
        {
            try
            {
                using var context = new MyDbContext();
                // Hash the password before saving
                staff.HashPassword();
                // Set role to Staff
                staff.Role = MONKEY5.BusinessObjects.Helpers.Role.Staff;
                context.Staffs.Add(staff);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateStaff(Staff staff)
        {
            try
            {
                using var context = new MyDbContext();
                
                // Get the existing staff from the database
                var existingStaff = context.Staffs.FirstOrDefault(s => s.UserId == staff.UserId);
                if (existingStaff == null)
                {
                    throw new Exception("Staff not found");
                }
                
                // Update properties
                existingStaff.FullName = staff.FullName;
                existingStaff.Email = staff.Email;
                existingStaff.PhoneNumber = staff.PhoneNumber;
                existingStaff.DateOfBirth = staff.DateOfBirth;
                existingStaff.Gender = staff.Gender;
                existingStaff.IdNumber = staff.IdNumber;
                existingStaff.Status = staff.Status;
                existingStaff.AvgRating = staff.AvgRating;
                
                // Always ensure the role is Staff
                existingStaff.Role = MONKEY5.BusinessObjects.Helpers.Role.Staff;
                
                // Check if password needs to be hashed (if it was changed)
                if (!string.IsNullOrEmpty(staff.Password))
                {
                    existingStaff.Password = staff.Password;
                    existingStaff.HashPassword();
                }
                
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteStaff(Staff staff)
        {
            try
            {
                using var context = new MyDbContext();
                var staffToDelete = context.Staffs.SingleOrDefault(s => s.UserId == staff.UserId);
                if (staffToDelete != null)
                {
                    context.Staffs.Remove(staffToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Staff? GetStaffById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Staffs.FirstOrDefault(s => s.UserId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Staff? GetStaffByPhone(string phone)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Staffs.FirstOrDefault(s => s.PhoneNumber.Equals(phone));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Staff> GetAvailableStaffs()
        {
            try
            {
                using var db = new MyDbContext();
                return db.Staffs
                    .Where(s => s.Status == MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Staff? Login(string email, string password)
        {
            try
            {
                using var db = new MyDbContext();
                var staff = db.Staffs.FirstOrDefault(s => s.Email.Equals(email));
                
                if (staff != null && !string.IsNullOrEmpty(staff.PasswordHash))
                {
                    bool isPasswordValid = MONKEY5.BusinessObjects.Helpers.PasswordHasher.VerifyPassword(password, staff.PasswordHash);
                    if (isPasswordValid)
                    {
                        return staff;
                    }
                }
                
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
