using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
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

            _leaveRequestService.DeleteLeaveRequest(leaveRequest);

            return NoContent();
        }
    }
}
