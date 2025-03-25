using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.Services;

namespace MONKEY5.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // ✅ GET: /api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // ✅ GET: /api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        // ✅ GET: /api/user/phone/{phoneNumber}
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetUserByPhone(string phoneNumber)
        {
            var user = await _userService.GetUserByPhoneAsync(phoneNumber);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        // ✅ POST: /api/user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null) return BadRequest("Invalid user data");

            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // ✅ PUT: /api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            if (user == null || id != user.UserId) return BadRequest("Invalid user data");

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null) return NotFound("User not found");

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // ✅ DELETE: /api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
