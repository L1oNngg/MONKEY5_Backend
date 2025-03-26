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
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController()
        {
            _paymentService = new PaymentService();
        }

        // GET: api/Payments
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return _paymentService.GetPayments();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(Guid id)
        {
            var payment = _paymentService.GetPaymentById(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // GET: api/Payments/booking/{bookingId}
        [HttpGet("booking/{bookingId}")]
        public ActionResult<Payment> GetPaymentByBookingId(Guid bookingId)
        {
            var payment = _paymentService.GetPaymentByBookingId(bookingId);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // GET: api/Payments/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Payment>> GetPaymentsByCustomerId(Guid customerId)
        {
            return _paymentService.GetPaymentsByCustomerId(customerId);
        }

        // GET: api/Payments/status/{status}
        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<Payment>> GetPaymentsByStatus(PaymentStatus status)
        {
            return _paymentService.GetPaymentsByStatus(status);
        }

        // GET: api/Payments/daterange
        [HttpGet("daterange")]
        public ActionResult<IEnumerable<Payment>> GetPaymentsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return _paymentService.GetPaymentsByDateRange(startDate, endDate);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public IActionResult PutPayment(Guid id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }

            _paymentService.UpdatePayment(payment);

            return NoContent();
        }

        // POST: api/Payments
        [HttpPost]
        public ActionResult<Payment> PostPayment(Payment payment)
        {
            _paymentService.SavePayment(payment);

            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(Guid id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }

            _paymentService.DeletePayment(payment);

            return NoContent();
        }
    }
}
