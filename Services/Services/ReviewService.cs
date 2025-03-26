using MONKEY5.BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewService()
        {
            reviewRepository = new ReviewRepository();
        }

        public List<Review> GetReviews() => reviewRepository.GetReviews();
        
        public void SaveReview(Review review) => reviewRepository.SaveReview(review);
        
        public void UpdateReview(Review review) => reviewRepository.UpdateReview(review);
        
        public void DeleteReview(Review review) => reviewRepository.DeleteReview(review);
        
        public Review GetReviewById(Guid id) => reviewRepository.GetReviewById(id);
        
        public List<Review> GetReviewsByStaffId(Guid staffId) => 
            reviewRepository.GetReviewsByStaffId(staffId);
        
        public List<Review> GetReviewsByCustomerId(Guid customerId) => 
            reviewRepository.GetReviewsByCustomerId(customerId);
        
        public Review GetReviewByBookingId(Guid bookingId) => 
            reviewRepository.GetReviewByBookingId(bookingId);
    }
}
