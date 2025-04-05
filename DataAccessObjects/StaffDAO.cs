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
                
                // Only update properties that are provided (not null)
                if (!string.IsNullOrEmpty(staff.FullName))
                    existingStaff.FullName = staff.FullName;
                    
                if (!string.IsNullOrEmpty(staff.Email))
                    existingStaff.Email = staff.Email;
                    
                if (!string.IsNullOrEmpty(staff.PhoneNumber))
                    existingStaff.PhoneNumber = staff.PhoneNumber;
                    
                if (staff.DateOfBirth != default(DateTime) && staff.DateOfBirth != null)
                    existingStaff.DateOfBirth = staff.DateOfBirth;
                    
                if (!string.IsNullOrEmpty(staff.Gender))
                    existingStaff.Gender = staff.Gender;
                    
                if (!string.IsNullOrEmpty(staff.IdNumber))
                    existingStaff.IdNumber = staff.IdNumber;
                    
                // For status, we need to check if it's not the default value
                if (staff.Status != default(MONKEY5.BusinessObjects.Helpers.AvailabilityStatus))
                    existingStaff.Status = staff.Status;
                    
                // For avgRating, check if it's not the default value (0)
                if (staff.AvgRating != 0)
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
        public static List<Staff> GetAvailableStaffsWithBookingCounts(DateTime monthStart, DateTime monthEnd)
        {
            try
            {
                using var db = new MyDbContext();
                
                // Get all available staff
                var availableStaffs = db.Staffs
                    .Where(s => s.Status == MONKEY5.BusinessObjects.Helpers.AvailabilityStatus.Available)
                    .ToList();
                    
                // For each staff, count their bookings this month
                foreach (var staff in availableStaffs)
                {
                    staff.BookingCount = db.Bookings
                        .Count(b => b.StaffId == staff.UserId && 
                                b.ServiceStartTime >= monthStart && 
                                b.ServiceStartTime <= monthEnd);
                }
                
                // Order by booking count (ascending)
                return availableStaffs.OrderBy(s => s.BookingCount).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
