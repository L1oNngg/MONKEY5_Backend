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
                // Log exception if needed
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
                // Check if password needs to be hashed (if it was changed)
                if (!string.IsNullOrEmpty(staff.Password))
                {
                    staff.HashPassword();
                }
                context.Entry<Staff>(staff).State = EntityState.Modified;
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

        public static Staff GetStaffById(Guid id)
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

        public static Staff GetStaffByEmail(string email)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Staffs.FirstOrDefault(s => s.Email.Equals(email));
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
    }
}
