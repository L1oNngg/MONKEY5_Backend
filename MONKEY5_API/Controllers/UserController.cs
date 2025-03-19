using Microsoft.AspNetCore.Mvc;
using MONKEY5.BusinessObjects;
using MONKEY5.Services;

namespace MONKEY5.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.RegisterUserAsync(user);
            return Ok(new { message = "User registered successfully!" });
        }
    }
}
