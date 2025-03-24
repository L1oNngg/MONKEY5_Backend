using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace DataAccessObjects
{
    public class ReviewDAO
    {
        public static List<Review> GetReviews()
        {
            var listReviews = new List<Review>();
            try
            {
                using var db = new MyDbContext();
                listReviews = db.Reviews
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .ToList();
            }
            catch (Exception e)
            {
                // Log exception if needed
            }
            return listReviews;
        }

        public static void SaveReview(Review review)
        {
            try
            {
                using var context = new MyDbContext();
                context.Reviews.Add(review);
                context.SaveChanges();
                
                // Update staff average rating
                UpdateStaffAverageRating(review.Booking.StaffId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateReview(Review review)
        {
            try
            {
                using var context = new MyDbContext();
                context.Entry<Review>(review).State = EntityState.Modified;
                context.SaveChanges();
                
                // Update staff average rating
                UpdateStaffAverageRating(review.Booking.StaffId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteReview(Review review)
        {
            try
            {
                using var context = new MyDbContext();
                var reviewToDelete = context.Reviews.SingleOrDefault(r => r.ReviewId == review.ReviewId);
                if (reviewToDelete != null)
                {
                    // Store staff ID before deleting for rating update
                    var staffId = context.Bookings
                        .Where(b => b.BookingId == reviewToDelete.BookingId)
                        .Select(b => b.StaffId)
                        .FirstOrDefault();
                    
                    context.Reviews.Remove(reviewToDelete);
                    context.SaveChanges();
                    
                    // Update staff average rating
                    if (staffId != Guid.Empty)
                    {
                        UpdateStaffAverageRating(staffId);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Review GetReviewById(Guid id)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Reviews
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .FirstOrDefault(r => r.ReviewId.Equals(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Review> GetReviewsByStaffId(Guid staffId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Reviews
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Where(r => r.Booking.StaffId == staffId)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Review> GetReviewsByCustomerId(Guid customerId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Reviews
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .Where(r => r.Booking.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Review GetReviewByBookingId(Guid bookingId)
        {
            try
            {
                using var db = new MyDbContext();
                return db.Reviews
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Staff)
                    .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                    .FirstOrDefault(r => r.BookingId == bookingId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void UpdateStaffAverageRating(Guid staffId)
        {
            try
            {
                using var context = new MyDbContext();
                var reviews = context.Reviews
                    .Include(r => r.Booking)
                    .Where(r => r.Booking.StaffId == staffId)
                    .ToList();

                if (reviews.Any())
                {
                    double avgRating = reviews.Average(r => r.RatingStar);
                    
                    var staff = context.Staffs.FirstOrDefault(s => s.UserId == staffId);
                    if (staff != null)
                    {
                        staff.AvgRating = avgRating;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                // Log error but don't throw to prevent cascading failures
            }
        }
    }
}
