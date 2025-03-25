using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MONKEY5.BusinessObjects;
using MONKEY5.DataAccessObjects;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletionReportsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CompletionReportsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CompletionReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletionReport>>> GetCompletionReports()
        {
            return await _context.CompletionReports.ToListAsync();
        }

        // GET: api/CompletionReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompletionReport>> GetCompletionReport(Guid id)
        {
            var completionReport = await _context.CompletionReports.FindAsync(id);

            if (completionReport == null)
            {
                return NotFound();
            }

            return completionReport;
        }

        // PUT: api/CompletionReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletionReport(Guid id, CompletionReport completionReport)
        {
            if (id != completionReport.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(completionReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletionReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CompletionReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompletionReport>> PostCompletionReport(CompletionReport completionReport)
        {
            _context.CompletionReports.Add(completionReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompletionReport", new { id = completionReport.ReportId }, completionReport);
        }

        // DELETE: api/CompletionReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompletionReport(Guid id)
        {
            var completionReport = await _context.CompletionReports.FindAsync(id);
            if (completionReport == null)
            {
                return NotFound();
            }

            _context.CompletionReports.Remove(completionReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompletionReportExists(Guid id)
        {
            return _context.CompletionReports.Any(e => e.ReportId == id);
        }
    }
}
