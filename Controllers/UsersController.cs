// Controllers/UsersController.cs

using Microsoft.AspNetCore.Mvc;
using simpleAPI.Services;
using simpleAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace simpleAPI.Controllers
{
    [Route("simpleAPI/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] User user, [FromQuery] string password)
        {
            if (user == null || string.IsNullOrEmpty(password))
                return BadRequest("User data or password is missing.");

            await _userService.AddUserAsync(user, password);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user, [FromQuery] string password)
        {
            if (user == null || id != user.Id)
                return BadRequest("User data is invalid.");

            await _userService.UpdateUserAsync(user, password);
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
