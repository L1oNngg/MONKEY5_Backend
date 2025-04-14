using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController()
        {
            _serviceService = new ServiceService();
        }

        // GET: api/Services
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return _serviceService.GetServices();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public ActionResult<Service> GetService(Guid id)
        {
            var service = _serviceService.GetServiceById(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // GET: api/Services/search/{name}
        [HttpGet("search/{name}")]
        public ActionResult<IEnumerable<Service>> SearchServicesByName(string name)
        {
            return _serviceService.SearchServicesByName(name);
        }

        // GET: api/Services/pricerange
        [HttpGet("pricerange")]
        public ActionResult<IEnumerable<Service>> GetServicesByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            return _serviceService.GetServicesByPriceRange(minPrice, maxPrice);
        }

        // PUT: api/Services/5
        [HttpPut("{id}")]
        public IActionResult PutService(Guid id, Service service)
        {
            if (id != service.ServiceId)
            {
                return BadRequest();
            }

            _serviceService.UpdateService(service);

            return NoContent();
        }

        // POST: api/Services
        [HttpPost]
        public ActionResult<Service> PostService(Service service)
        {
            _serviceService.SaveService(service);

            return CreatedAtAction("GetService", new { id = service.ServiceId }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public IActionResult DeleteService(Guid id)
        {
            var service = _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            try
            {
                _serviceService.DeleteService(service);
            }
            catch (Exception ex)
            {
                // Check for foreign key violation (Postgres/SQLServer/SQLite all have different codes, so use message as fallback)
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete service because it is referenced by other records (e.g., bookings)." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete service because it is referenced by other records (e.g., bookings)." });
                }
                // Fallback: generic error
                return Conflict(new { error = "Failed to delete service. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }
    }
}
