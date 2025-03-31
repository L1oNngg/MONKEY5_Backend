using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.DTOs;
using MONKEY5.BusinessObjects.DTOs.Mappers;
using MONKEY5.BusinessObjects.Helpers;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<IEnumerable<BookingDTO>> GetBookings()
        {
            var bookings = _bookingService.GetBookings();
            return bookings.ToBookingDtoList();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public ActionResult<BookingDTO> GetBooking(Guid id)
        {
            var booking = _bookingService.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking.ToBookingDto();
        }

        // GET: api/Bookings/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<BookingDTO>> GetBookingsByCustomerId(Guid customerId)
        {
            var bookings = _bookingService.GetBookingsByCustomerId(customerId);
            return bookings.ToBookingDtoList();
        }

        // GET: api/Bookings/staff/{staffId}
        [HttpGet("staff/{staffId}")]
        public ActionResult<IEnumerable<BookingDTO>> GetBookingsByStaffId(Guid staffId)
        {
            var bookings = _bookingService.GetBookingsByStaffId(staffId);
            return bookings.ToBookingDtoList();
        }

        // GET: api/Bookings/status/{status}
        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<BookingDTO>> GetBookingsByStatus(OrderStatus status)
        {
            var bookings = _bookingService.GetBookingsByStatus(status);
            return bookings.ToBookingDtoList();
        }

        // GET: api/Bookings/daterange
        [HttpGet("daterange")]
        public ActionResult<IEnumerable<BookingDTO>> GetBookingsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var bookings = _bookingService.GetBookingsByDateRange(startDate, endDate);
            return bookings.ToBookingDtoList();
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
        public ActionResult<BookingDTO> PostBooking(Booking booking)
        {
            _bookingService.SaveBooking(booking);

            var createdBooking = _bookingService.GetBookingById(booking.BookingId);
            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, createdBooking.ToBookingDto());
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
