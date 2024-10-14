using Application.Commands.Menus;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<int>> CreateMenu([FromBody] CreateMenuCommand command)
        {
            var createdMenuId = await _menuAppService.CreateMenuAsync(command);
            return CreatedAtAction(nameof(GetMenuById), new { id = createdMenuId }, createdMenuId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetMenuById(int id)
        {
            var menu = await _menuAppService.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetAllMenus()
        {
            var menus = await _menuAppService.GetAllMenusAsync();
            return Ok(menus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] UpdateMenuCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Menu ID mismatch");
            }

            await _menuAppService.UpdateMenuAsync(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _menuAppService.DeleteMenuAsync(new DeleteMenuCommand(id));
            return NoContent();
        }
    }
}