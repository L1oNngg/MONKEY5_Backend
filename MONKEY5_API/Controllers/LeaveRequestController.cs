using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using BusinessObjects.Helpers;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController()
        {
            _leaveRequestService = new LeaveRequestService();
        }

        // GET: api/LeaveRequests
        [HttpGet]
        public ActionResult<IEnumerable<LeaveRequest>> GetLeaveRequests()
        {
            return _leaveRequestService.GetLeaveRequests();
        }

        // GET: api/LeaveRequests/5
        [HttpGet("{id}")]
        public ActionResult<LeaveRequest> GetLeaveRequest(Guid id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return leaveRequest;
        }

        // GET: api/LeaveRequests/staff/{staffId}
        [HttpGet("staff/{staffId}")]
        public ActionResult<IEnumerable<LeaveRequest>> GetLeaveRequestsByStaffId(Guid staffId)
        {
            return _leaveRequestService.GetLeaveRequestsByStaffId(staffId);
        }

        // GET: api/LeaveRequests/daterange
        [HttpGet("daterange")]
        public ActionResult<IEnumerable<LeaveRequest>> GetLeaveRequestsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return _leaveRequestService.GetLeaveRequestsByDateRange(startDate, endDate);
        }

        // PUT: api/LeaveRequests/5
        [HttpPut("{id}")]
        public IActionResult PutLeaveRequest(Guid id, LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.RequestId)
            {
                return BadRequest();
            }

            _leaveRequestService.UpdateLeaveRequest(leaveRequest);

            return NoContent();
        }

        // POST: api/LeaveRequests
        [HttpPost]
        public ActionResult<LeaveRequest> PostLeaveRequest(LeaveRequest leaveRequest)
        {
            _leaveRequestService.SaveLeaveRequest(leaveRequest);

            return CreatedAtAction("GetLeaveRequest", new { id = leaveRequest.RequestId }, leaveRequest);
        }

        // DELETE: api/LeaveRequests/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveRequest(Guid id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            try
            {
                _leaveRequestService.DeleteLeaveRequest(leaveRequest);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete leave request because it is referenced by other records." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete leave request because it is referenced by other records." });
                }
                return Conflict(new { error = "Failed to delete leave request. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }
    
        // PUT: api/LeaveRequests/5/status
        [HttpPut("{id}/status")]
        public IActionResult UpdateLeaveRequestStatus(Guid id, [FromBody] LeaveRequestStatus status)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
    
            leaveRequest.Status = status;
            _leaveRequestService.UpdateLeaveRequest(leaveRequest);
    
            return NoContent();
        }
    }
}
