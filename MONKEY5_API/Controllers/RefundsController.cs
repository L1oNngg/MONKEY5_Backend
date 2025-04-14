using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundsController : ControllerBase
    {
        private readonly IRefundService _refundService;

        public RefundsController()
        {
            _refundService = new RefundService();
        }

        // GET: api/Refunds
        [HttpGet]
        public ActionResult<IEnumerable<Refund>> GetRefunds()
        {
            return _refundService.GetRefunds();
        }

        // GET: api/Refunds/5
        [HttpGet("{id}")]
        public ActionResult<Refund> GetRefund(Guid id)
        {
            var refund = _refundService.GetRefundById(id);

            if (refund == null)
            {
                return NotFound();
            }

            return refund;
        }

        // GET: api/Refunds/payment/{paymentId}
        [HttpGet("payment/{paymentId}")]
        public ActionResult<Refund> GetRefundByPaymentId(Guid paymentId)
        {
            var refund = _refundService.GetRefundByPaymentId(paymentId);

            if (refund == null)
            {
                return NotFound();
            }

            return refund;
        }

        // GET: api/Refunds/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Refund>> GetRefundsByCustomerId(Guid customerId)
        {
            return _refundService.GetRefundsByCustomerId(customerId);
        }

        // GET: api/Refunds/daterange
        [HttpGet("daterange")]
        public ActionResult<IEnumerable<Refund>> GetRefundsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return _refundService.GetRefundsByDateRange(startDate, endDate);
        }

        // PUT: api/Refunds/5
        [HttpPut("{id}")]
        public IActionResult PutRefund(Guid id, Refund refund)
        {
            if (id != refund.RefundId)
            {
                return BadRequest();
            }

            _refundService.UpdateRefund(refund);

            return NoContent();
        }

        // POST: api/Refunds
        [HttpPost]
        public ActionResult<Refund> PostRefund(Refund refund)
        {
            _refundService.SaveRefund(refund);

            return CreatedAtAction("GetRefund", new { id = refund.RefundId }, refund);
        }

        // DELETE: api/Refunds/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRefund(Guid id)
        {
            var refund = _refundService.GetRefundById(id);
            if (refund == null)
            {
                return NotFound();
            }

            try
            {
                _refundService.DeleteRefund(refund);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete refund because it is referenced by other records." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete refund because it is referenced by other records." });
                }
                return Conflict(new { error = "Failed to delete refund. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }
    }
}
