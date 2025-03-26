﻿using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagersController()
        {
            _managerService = new ManagerService();
        }

        // GET: api/Managers
        [HttpGet]
        public ActionResult<IEnumerable<Manager>> GetManagers()
        {
            return _managerService.GetManagers();
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public ActionResult<Manager> GetManager(Guid id)
        {
            var manager = _managerService.GetManagerById(id);

            if (manager == null)
            {
                return NotFound();
            }

            return manager;
        }

        // GET: api/Managers/email/{email}
        [HttpGet("email/{email}")]
        public ActionResult<Manager> GetManagerByEmail(string email)
        {
            var manager = _managerService.GetManagerByEmail(email);

            if (manager == null)
            {
                return NotFound();
            }

            return manager;
        }

        // PUT: api/Managers/5
        [HttpPut("{id}")]
        public IActionResult PutManager(Guid id, Manager manager)
        {
            if (id != manager.UserId)
            {
                return BadRequest();
            }

            _managerService.UpdateManager(manager);

            return NoContent();
        }

        // POST: api/Managers
        [HttpPost]
        public ActionResult<Manager> PostManager(Manager manager)
        {
            _managerService.SaveManager(manager);

            return CreatedAtAction("GetManager", new { id = manager.UserId }, manager);
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteManager(Guid id)
        {
            var manager = _managerService.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }

            _managerService.DeleteManager(manager);

            return NoContent();
        }
    }
}
