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
    public class ReportImagesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ReportImagesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportImage>>> GetReportImages()
        {
            return await _context.ReportImages.ToListAsync();
        }

        // GET: api/ReportImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportImage>> GetReportImage(Guid id)
        {
            var reportImage = await _context.ReportImages.FindAsync(id);

            if (reportImage == null)
            {
                return NotFound();
            }

            return reportImage;
        }

        // PUT: api/ReportImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportImage(Guid id, ReportImage reportImage)
        {
            if (id != reportImage.ReportImageId)
            {
                return BadRequest();
            }

            _context.Entry(reportImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportImageExists(id))
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

        // POST: api/ReportImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportImage>> PostReportImage(ReportImage reportImage)
        {
            _context.ReportImages.Add(reportImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportImage", new { id = reportImage.ReportImageId }, reportImage);
        }

        // DELETE: api/ReportImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportImage(Guid id)
        {
            var reportImage = await _context.ReportImages.FindAsync(id);
            if (reportImage == null)
            {
                return NotFound();
            }

            _context.ReportImages.Remove(reportImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportImageExists(Guid id)
        {
            return _context.ReportImages.Any(e => e.ReportImageId == id);
        }
    }
}
