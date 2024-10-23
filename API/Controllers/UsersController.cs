using Application.Commands.Users;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>> GetAllUsers()
        {
            var users = await _userAppService.GetAllUsersAsync();
            var response = new ApiResponse<IEnumerable<UserResponseDto>>(true, "Users retrieved successfully", users, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A UserResponseDto representing the requested user.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetUserById(int id)
        {
            var user = await _userAppService.GetUserByIdAsync(id);
            if (user == null)
            {
                var errorResponse = new ApiResponse<UserResponseDto>(false, "User not found", null, 404);
                return NotFound(errorResponse);
            }

            var response = new ApiResponse<UserResponseDto>(true, "User retrieved successfully", user, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The UserRequestDto containing the new user's details.</param>
        /// <returns>The ID of the newly created user.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateUser([FromBody] UserRequestDto userDto)
        {
            var createdUserId = await _userAppService.CreateUserAsync(new CreateUserCommand(userDto));
            var response = new ApiResponse<int>(true, "User created successfully", createdUserId, 201);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUserId }, response);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The UserRequestDto containing the updated user's details.</param>
        /// <returns>No content if the update is successful, or a BadRequest if there's a mismatch in IDs.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateUser(int id, [FromBody] UserRequestDto userDto)
        {
            if (id <= 0)
            {
                var errorResponse = new ApiResponse<string>(false, "Invalid User ID", null, 400);
                return BadRequest(errorResponse);
            }

            await _userAppService.UpdateUserAsync(new UpdateUserCommand(id, userDto));
            var response = new ApiResponse<string>(true, "User updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteUser(int id)
        {
            if (id <= 0)
            {
                var errorResponse = new ApiResponse<string>(false, "Invalid User ID", null, 400);
                return BadRequest(errorResponse);
            }

            await _userAppService.DeleteUserAsync(new DeleteUserCommand(id));
            var response = new ApiResponse<string>(true, "User deleted successfully", null, 204);
            return Ok(response);
        }
    }
}