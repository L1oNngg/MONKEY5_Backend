using MONKEY5.BusinessObjects;
using System;
using System.Collections.Generic;

namespace Services
{
    public interface IReviewService
    {
        List<Review> GetReviews();
        void SaveReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
        Review GetReviewById(Guid id);
        List<Review> GetReviewsByStaffId(Guid staffId);
        List<Review> GetReviewsByCustomerId(Guid customerId);
        Review GetReviewByBookingId(Guid bookingId);
    }
}
