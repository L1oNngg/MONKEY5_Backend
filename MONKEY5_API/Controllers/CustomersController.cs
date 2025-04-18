﻿﻿using BusinessObjects.DTOs;
using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.BusinessObjects.DTOs;
using Services;
using System;
using System.Collections.Generic;

namespace MONKEY5_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController()
        {
            _customerService = new CustomerService();
        }

        // GET: api/Customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _customerService.GetCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(Guid id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/email/{email}
        [HttpGet("email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email)
        {
            var customer = _customerService.GetCustomerByEmail(email);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.UserId)
            {
                return BadRequest();
            }

            _customerService.UpdateCustomer(customer);

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            _customerService.SaveCustomer(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.UserId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            try
            {
                _customerService.DeleteCustomer(customer);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete customer because it is referenced by other records." });
                }
                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return Conflict(new { error = "Cannot delete customer because it is referenced by other records." });
                }
                return Conflict(new { error = "Failed to delete customer. It may be referenced by other records. Details: " + ex.Message });
            }

            return NoContent();
        }

        // POST: api/Customers/login
        [HttpPost("login")]
        public ActionResult<Customer> Login(LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest("Email and password are required");
            }

            var customer = _customerService.Login(loginDTO.Email, loginDTO.Password);
            
            if (customer == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return customer;
        }

        // GET: api/Customers/5/locations
        [HttpGet("{id}/locations")]
        public ActionResult<ICollection<LocationDTO>> GetCustomerLocations(Guid id)
        {
            var locations = _customerService.GetCustomerLocations(id);
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }

            return Ok(locations);
        }

        // POST: api/Customers/5/locations
        [HttpPost("{customerId}/locations")]
        public ActionResult<LocationDTO> PostCustomerLocation(Guid customerId, LocationDTO locationDto)
        {
            _customerService.AddLocationToCustomer(customerId, locationDto);
            return CreatedAtAction(nameof(GetCustomerLocations), new { id = customerId }, locationDto);
        }
    }
}
