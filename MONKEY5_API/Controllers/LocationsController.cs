using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController()
        {
            _locationService = new LocationService();
        }

        // GET: api/Locations
        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetLocations()
        {
            return _locationService.GetLocations();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public ActionResult<Location> GetLocation(Guid id)
        {
            var location = _locationService.GetLocationById(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // GET: api/Locations/city/{city}
        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<Location>> SearchLocationsByCity(string city)
        {
            return _locationService.SearchLocationsByCity(city);
        }

        // GET: api/Locations/address/{address}
        [HttpGet("address/{address}")]
        public ActionResult<IEnumerable<Location>> SearchLocationsByAddress(string address)
        {
            return _locationService.SearchLocationsByAddress(address);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public IActionResult PutLocation(Guid id, Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest();
            }

            _locationService.UpdateLocation(location);

            return NoContent();
        }

        // POST: api/Locations
        [HttpPost]
        public ActionResult<Location> PostLocation(Location location)
        {
            _locationService.SaveLocation(location);

            return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(Guid id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }

            _locationService.DeleteLocation(location);

            return NoContent();
        }
    }
}
