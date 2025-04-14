using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController()
        {
            _reviewService = new ReviewService();
        }

        // GET: api/Reviews
        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetReviews()
        {
            return _reviewService.GetReviews();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public ActionResult<Review> GetReview(Guid id)
        {
            var review = _reviewService.GetReviewById(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // GET: api/Reviews/staff/{staffId}
        [HttpGet("staff/{staffId}")]
        public ActionResult<IEnumerable<Review>> GetReviewsByStaffId(Guid staffId)
        {
            return _reviewService.GetReviewsByStaffId(staffId);
        }

        // GET: api/Reviews/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Review>> GetReviewsByCustomerId(Guid customerId)
        {
            return _reviewService.GetReviewsByCustomerId(customerId);
        }

        // GET: api/Reviews/booking/{bookingId}
        [HttpGet("booking/{bookingId}")]
        public ActionResult<Review> GetReviewByBookingId(Guid bookingId)
        {
            var review = _reviewService.GetReviewByBookingId(bookingId);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public IActionResult PutReview(Guid id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            _reviewService.UpdateReview(review);

            return NoContent();
        }

        // POST: api/Reviews
        [HttpPost]
        public ActionResult<Review> PostReview(Review review)
        {
            _reviewService.SaveReview(review);

            return CreatedAtAction("GetReview", new { id = review.ReviewId }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(Guid id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            try
            {
                _reviewService.DeleteReview(review);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete review because it is referenced by other records." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete review because it is referenced by other records." });
                }
                return Conflict(new { error = "Failed to delete review. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }
    }
}
