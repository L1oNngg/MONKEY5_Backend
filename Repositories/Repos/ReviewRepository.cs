using MONKEY5.BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;

namespace Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public List<Review> GetReviews() => ReviewDAO.GetReviews();
        
        public void SaveReview(Review review) => ReviewDAO.SaveReview(review);
        
        public void UpdateReview(Review review) => ReviewDAO.UpdateReview(review);
        
        public void DeleteReview(Review review) => ReviewDAO.DeleteReview(review);
        
        public Review GetReviewById(Guid id) => ReviewDAO.GetReviewById(id);
        
        public List<Review> GetReviewsByStaffId(Guid staffId) => ReviewDAO.GetReviewsByStaffId(staffId);
        
        public List<Review> GetReviewsByCustomerId(Guid customerId) => ReviewDAO.GetReviewsByCustomerId(customerId);
        
        public Review GetReviewByBookingId(Guid bookingId) => ReviewDAO.GetReviewByBookingId(bookingId);
    }
}
