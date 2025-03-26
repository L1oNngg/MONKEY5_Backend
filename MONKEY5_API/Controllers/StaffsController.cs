using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController()
        {
            _staffService = new StaffService();
        }

        // GET: api/Staffs
        [HttpGet]
        public ActionResult<IEnumerable<Staff>> GetStaffs()
        {
            return _staffService.GetStaffs();
        }

        // GET: api/Staffs/available
        [HttpGet("available")]
        public ActionResult<IEnumerable<Staff>> GetAvailableStaffs()
        {
            return _staffService.GetAvailableStaffs();
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public ActionResult<Staff> GetStaff(Guid id)
        {
            var staff = _staffService.GetStaffById(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // GET: api/Staffs/phone/{phoneNumber}
        [HttpGet("phone/{phoneNumber}")]
        public ActionResult<Staff> GetStaffByPhone(string phoneNumber)
        {
            var staff = _staffService.GetStaffByPhone(phoneNumber);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // PUT: api/Staffs/5
        [HttpPut("{id}")]
        public IActionResult PutStaff(Guid id, Staff staff)
        {
            if (id != staff.UserId)
            {
                return BadRequest();
            }

            _staffService.UpdateStaff(staff);

            return NoContent();
        }

        // POST: api/Staffs
        [HttpPost]
        public ActionResult<Staff> PostStaff(Staff staff)
        {
            _staffService.SaveStaff(staff);

            return CreatedAtAction("GetStaff", new { id = staff.UserId }, staff);
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(Guid id)
        {
            var staff = _staffService.GetStaffById(id);
            if (staff == null)
            {
                return NotFound();
            }

            _staffService.DeleteStaff(staff);

            return NoContent();
        }
    }
}
