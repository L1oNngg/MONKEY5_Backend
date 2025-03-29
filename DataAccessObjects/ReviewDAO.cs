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
                
                // Save the review
                context.Reviews.Add(review);
                context.SaveChanges();
                
                // Get the booking to access StaffId
                if (review.BookingId.HasValue)
                {
                    var booking = context.Bookings.FirstOrDefault(b => b.BookingId == review.BookingId);
                    if (booking != null)
                    {
                        // Update staff average rating
                        UpdateStaffAverageRating((Guid)booking.StaffId);
                    }
                }
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
                
                // Get the booking to access StaffId
                if (review.BookingId.HasValue)
                {
                    var booking = context.Bookings.FirstOrDefault(b => b.BookingId == review.BookingId);
                    if (booking != null)
                    {
                        // Update staff average rating
                        UpdateStaffAverageRating((Guid)booking.StaffId);
                    }
                }
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
                    // Store booking ID before deleting for rating update
                    Guid? bookingId = reviewToDelete.BookingId;
                    Guid staffId = Guid.Empty;
                    
                    if (bookingId.HasValue)
                    {
                        var booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                        if (booking != null)
                        {
                            staffId = (Guid)booking.StaffId;
                        }
                    }
                    
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
                    .Where(r => r.Booking != null && r.Booking.StaffId == staffId)
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
                    .FirstOrDefault(r => r.BookingId.HasValue && r.BookingId.Value == bookingId);
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
                    .Where(r => r.BookingId.HasValue)
                    .Join(
                        context.Bookings,
                        r => r.BookingId,
                        b => b.BookingId,
                        (r, b) => new { Review = r, Booking = b }
                    )
                    .Where(rb => rb.Booking.StaffId == staffId && rb.Review.RatingStar.HasValue)
                    .Select(rb => rb.Review)
                    .ToList();

                if (reviews.Any())
                {
                    double avgRating = reviews.Average(r => r.RatingStar.Value);
                    
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
