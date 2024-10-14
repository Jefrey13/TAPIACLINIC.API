using Application.Commands.Users;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userAppService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userAppService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserDto userDto)
        {
            var createdUserId = await _userAppService.CreateUserAsync(new CreateUserCommand(userDto));
            return CreatedAtAction(nameof(GetUserById), new { id = createdUserId }, createdUserId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("User ID in the request does not match the one in the body.");
            }

            await _userAppService.UpdateUserAsync(new UpdateUserCommand(id, userDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userAppService.DeleteUserAsync(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}