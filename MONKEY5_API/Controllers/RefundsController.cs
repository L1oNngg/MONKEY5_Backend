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
    public class RefundsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RefundsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Refunds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Refund>>> GetRefunds()
        {
            return await _context.Refunds.ToListAsync();
        }

        // GET: api/Refunds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Refund>> GetRefund(Guid id)
        {
            var refund = await _context.Refunds.FindAsync(id);

            if (refund == null)
            {
                return NotFound();
            }

            return refund;
        }

        // PUT: api/Refunds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefund(Guid id, Refund refund)
        {
            if (id != refund.RefundId)
            {
                return BadRequest();
            }

            _context.Entry(refund).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefundExists(id))
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

        // POST: api/Refunds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Refund>> PostRefund(Refund refund)
        {
            _context.Refunds.Add(refund);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefund", new { id = refund.RefundId }, refund);
        }

        // DELETE: api/Refunds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefund(Guid id)
        {
            var refund = await _context.Refunds.FindAsync(id);
            if (refund == null)
            {
                return NotFound();
            }

            _context.Refunds.Remove(refund);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefundExists(Guid id)
        {
            return _context.Refunds.Any(e => e.RefundId == id);
        }
    }
}
