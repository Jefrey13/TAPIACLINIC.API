using Application.Commands.Users;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userAppService">The application service responsible for user-related operations.</param>
        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>A list of UserResponseDto representing all users.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userAppService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A UserResponseDto representing the requested user.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            var user = await _userAppService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The UserRequestDto containing the new user's details.</param>
        /// <returns>The ID of the newly created user.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserRequestDto userDto)
        {
            var createdUserId = await _userAppService.CreateUserAsync(new CreateUserCommand(userDto));
            return CreatedAtAction(nameof(GetUserById), new { id = createdUserId }, createdUserId);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The UserRequestDto containing the updated user's details.</param>
        /// <returns>No content if the update is successful, or a BadRequest if there's a mismatch in IDs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequestDto userDto)
        {
            // Verifica si el objeto userDto es nulo
            if (id == null)
            {
                return BadRequest("User id must not be null.");
            }

            await _userAppService.UpdateUserAsync(new UpdateUserCommand(id, userDto));
            return NoContent();
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userAppService.DeleteUserAsync(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}