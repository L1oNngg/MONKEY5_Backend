using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.Services;

namespace MONKEY5.API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceService _serviceService;

        public ServiceController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // ✅ GET: /api/service
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        // ✅ GET: /api/service/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null) return NotFound("Service not found");
            return Ok(service);
        }

        // ✅ GET: /api/service/search/{name}
        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchServicesByName(string name)
        {
            var services = await _serviceService.SearchServicesByNameAsync(name);
            return Ok(services);
        }

        // ✅ GET: /api/service/price?min=100&max=500
        [HttpGet("price")]
        public async Task<IActionResult> GetServicesByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var services = await _serviceService.GetServicesByPriceRangeAsync(min, max);
            return Ok(services);
        }

        // ✅ POST: /api/service
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] Service service)
        {
            if (service == null) return BadRequest("Invalid service data");

            await _serviceService.AddServiceAsync(service);
            return CreatedAtAction(nameof(GetServiceById), new { id = service.ServiceId }, service);
        }

        // ✅ PUT: /api/service/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] Service service)
        {
            if (service == null || id != service.ServiceId) return BadRequest("Invalid service data");

            var existingService = await _serviceService.GetServiceByIdAsync(id);
            if (existingService == null) return NotFound("Service not found");

            await _serviceService.UpdateServiceAsync(service);
            return NoContent();
        }

        // ✅ DELETE: /api/service/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null) return NotFound("Service not found");

            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
    }
}
