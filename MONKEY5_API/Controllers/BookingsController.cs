using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.Helpers;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController()
        {
            _bookingService = new BookingService();
        }

        // GET: api/Bookings
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetBookings()
        {
            return _bookingService.GetBookings();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(Guid id)
        {
            var booking = _bookingService.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // GET: api/Bookings/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Booking>> GetBookingsByCustomerId(Guid customerId)
        {
            return _bookingService.GetBookingsByCustomerId(customerId);
        }

        // GET: api/Bookings/staff/{staffId}
        [HttpGet("staff/{staffId}")]
        public ActionResult<IEnumerable<Booking>> GetBookingsByStaffId(Guid staffId)
        {
            return _bookingService.GetBookingsByStaffId(staffId);
        }

        // GET: api/Bookings/status/{status}
        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<Booking>> GetBookingsByStatus(OrderStatus status)
        {
            return _bookingService.GetBookingsByStatus(status);
        }

        // GET: api/Bookings/daterange
        [HttpGet("daterange")]
        public ActionResult<IEnumerable<Booking>> GetBookingsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return _bookingService.GetBookingsByDateRange(startDate, endDate);
        }

        // GET: api/Bookings/staffavailable
        [HttpGet("staffavailable")]
        public ActionResult<bool> IsStaffAvailable([FromQuery] Guid staffId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            return _bookingService.IsStaffAvailable(staffId, startTime, endTime);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public IActionResult PutBooking(Guid id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _bookingService.UpdateBooking(booking);

            return NoContent();
        }

        // POST: api/Bookings
        [HttpPost]
        public ActionResult<Booking> PostBooking(Booking booking)
        {
            _bookingService.SaveBooking(booking);

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(Guid id)
        {
            var booking = _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingService.DeleteBooking(booking);

            return NoContent();
        }
    }
}
