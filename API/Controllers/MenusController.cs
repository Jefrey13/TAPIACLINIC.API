using Application.Commands.Menus;
using Application.Models;
using Application.Models.ResponseDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.ReponseDtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuAppService _menuAppService;

        public MenusController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        /// <summary>
        /// Creates a new menu.
        /// </summary>
        /// <param name="command">The command containing menu details.</param>
        /// <returns>The ID of the newly created menu.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateMenu([FromBody] CreateMenuCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<int>(false, "Menu data is required", default, 400));
            }

            var createdMenuId = await _menuAppService.CreateMenuAsync(command);
            var response = new ApiResponse<int>(true, "Menu created successfully", createdMenuId, 201);
            return CreatedAtAction(nameof(GetMenuById), new { id = createdMenuId }, response);
        }

        /// <summary>
        /// Retrieves a specific menu by its ID.
        /// </summary>
        /// <param name="id">The ID of the menu.</param>
        /// <returns>The MenuDto of the requested menu, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MenuDto>>> GetMenuById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<MenuDto>(false, "Invalid menu ID", null, 400));
            }

            var menu = await _menuAppService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound(new ApiResponse<MenuDto>(false, "Menu not found", null, 404));
            }

            var response = new ApiResponse<MenuDto>(true, "Menu retrieved successfully", menu, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all menus.
        /// </summary>
        /// <returns>A list of all menus.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<MenuDto>>>> GetAllMenus()
        {
            var menus = await _menuAppService.GetAllMenusAsync();
            if (menus == null || !menus.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<MenuDto>>(false, "No menus found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<MenuDto>>(true, "Menus retrieved successfully", menus, 200);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing menu.
        /// </summary>
        /// <param name="id">The ID of the menu to update.</param>
        /// <param name="command">The command containing updated menu details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateMenu(int id, [FromBody] UpdateMenuCommand command)
        {
            if (id <= 0 || command == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid menu ID or data", null, 400));
            }

            await _menuAppService.UpdateMenuAsync(command);
            var response = new ApiResponse<string>(true, "Menu updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a menu by its ID.
        /// </summary>
        /// <param name="id">The ID of the menu to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteMenu(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid menu ID", null, 400));
            }

            var menu = await _menuAppService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound(new ApiResponse<string>(false, "Menu not found", null, 404));
            }

            await _menuAppService.DeleteMenuAsync(new DeleteMenuCommand(id));
            var response = new ApiResponse<string>(true, "Menu deleted successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves menus based on the role associated with the user.
        /// </summary>
        /// <returns>
        /// An ApiResponse containing a list of MenuResponseDto objects, representing the menus accessible to the user's role.
        /// </returns>
        [HttpGet("by-role")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MenuResponseDto>>>> GetMenusByRole()
        {
            // Get JWT from the Authorization header
            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Check if the token is provided
            if (string.IsNullOrEmpty(jwtToken))
            {
                return ResponseHelper.Unauthorized<IEnumerable<MenuResponseDto>>("Authorization token is missing");
            }

            try
            {
                // Call the service to get menus based on the user's role
                var menus = await _menuAppService.GetMenusByRoleAsync(jwtToken);

                // Check if menus are found
                if (menus == null || !menus.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<MenuResponseDto>>("No menus found for this role");
                }

                // Return a success response with the menus
                return ResponseHelper.Success(menus, "Menus retrieved based on role successfully");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                Console.WriteLine($"Exception: {ex.Message}");
                return ResponseHelper.Error<IEnumerable<MenuResponseDto>>($"Error: {ex.Message}");
            }
        }
    }
}