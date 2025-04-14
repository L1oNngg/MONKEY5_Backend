using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletionReportsController : ControllerBase
    {
        private readonly ICompletionReportService _completionReportService;

        public CompletionReportsController()
        {
            _completionReportService = new CompletionReportService();
        }

        // GET: api/CompletionReports
        [HttpGet]
        public ActionResult<IEnumerable<CompletionReport>> GetCompletionReports()
        {
            return _completionReportService.GetCompletionReports();
        }

        // GET: api/CompletionReports/5
        [HttpGet("{id}")]
        public ActionResult<CompletionReport> GetCompletionReport(Guid id)
        {
            var completionReport = _completionReportService.GetCompletionReportById(id);

            if (completionReport == null)
            {
                return NotFound();
            }

            return completionReport;
        }

        // GET: api/CompletionReports/booking/{bookingId}
        [HttpGet("booking/{bookingId}")]
        public ActionResult<CompletionReport> GetCompletionReportByBookingId(Guid bookingId)
        {
            var completionReport = _completionReportService.GetCompletionReportByBookingId(bookingId);

            if (completionReport == null)
            {
                return NotFound();
            }

            return completionReport;
        }

        // GET: api/CompletionReports/staff/{staffId}
        [HttpGet("staff/{staffId}")]
        public ActionResult<IEnumerable<CompletionReport>> GetCompletionReportsByStaffId(Guid staffId)
        {
            return _completionReportService.GetCompletionReportsByStaffId(staffId);
        }

        // GET: api/CompletionReports/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<CompletionReport>> GetCompletionReportsByCustomerId(Guid customerId)
        {
            return _completionReportService.GetCompletionReportsByCustomerId(customerId);
        }

        // GET: api/CompletionReports/daterange
        [HttpGet("daterange")]
        public ActionResult<IEnumerable<CompletionReport>> GetCompletionReportsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return _completionReportService.GetCompletionReportsByDateRange(startDate, endDate);
        }

        // PUT: api/CompletionReports/5
        [HttpPut("{id}")]
        public IActionResult PutCompletionReport(Guid id, CompletionReport completionReport)
        {
            if (id != completionReport.ReportId)
            {
                return BadRequest();
            }

            _completionReportService.UpdateCompletionReport(completionReport);

            return NoContent();
        }

        // POST: api/CompletionReports
        [HttpPost]
        public ActionResult<CompletionReport> PostCompletionReport(CompletionReport completionReport)
        {
            _completionReportService.SaveCompletionReport(completionReport);

            return CreatedAtAction("GetCompletionReport", new { id = completionReport.ReportId }, completionReport);
        }

        // DELETE: api/CompletionReports/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompletionReport(Guid id)
        {
            var completionReport = _completionReportService.GetCompletionReportById(id);
            if (completionReport == null)
            {
                return NotFound();
            }

            try
            {
                _completionReportService.DeleteCompletionReport(completionReport);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete completion report because it is referenced by other records." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete completion report because it is referenced by other records." });
                }
                return Conflict(new { error = "Failed to delete completion report. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }
    }
}
