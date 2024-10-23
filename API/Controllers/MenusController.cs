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
            var menu = await _menuAppService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                var errorResponse = new ApiResponse<MenuDto>(false, "Menu not found", null, 404);
                return NotFound(errorResponse);
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
            if (id != command.Id)
            {
                var errorResponse = new ApiResponse<string>(false, "Menu ID mismatch", null, 400);
                return BadRequest(errorResponse);
            }

            await _menuAppService.UpdateMenuAsync(command);
            var response = new ApiResponse<string>(true, "Menu updated successfully", null, 204);
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
            await _menuAppService.DeleteMenuAsync(new DeleteMenuCommand(id));
            var response = new ApiResponse<string>(true, "Menu deleted successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves menus based on the role associated with the user.
        /// </summary>
        /// <returns>A list of menus accessible to the role of the user.</returns>
        [HttpGet("by-role")]
        public async Task<ActionResult<ApiResponse<IEnumerable<MenuResponseDto>>>> GetMenusByRole()
        {
            // Get JWT from the Authorization header
            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Call the service to get menus based on the user's role
            var menus = await _menuAppService.GetMenusByRoleAsync(jwtToken);
            var response = new ApiResponse<IEnumerable<MenuResponseDto>>(true, "Menus retrieved based on role successfully", menus, 200);
            return Ok(response);
        }
    }
}