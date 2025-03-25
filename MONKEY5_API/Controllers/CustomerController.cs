using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.Services;

namespace MONKEY5.API.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // ✅ GET: /api/customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // ✅ GET: /api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Customer not found");
            return Ok(customer);
        }

        // ✅ GET: /api/customer/email/{email}
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var customer = await _customerService.GetCustomerByEmailAsync(email);
            if (customer == null) return NotFound("Customer not found");
            return Ok(customer);
        }

        // ✅ POST: /api/customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest("Invalid customer data");

            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.UserId }, customer);
        }

        // ✅ PUT: /api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            if (customer == null || id != customer.UserId) return BadRequest("Invalid customer data");

            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null) return NotFound("Customer not found");

            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        // ✅ DELETE: /api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Customer not found");

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
