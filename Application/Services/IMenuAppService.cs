using Application.Commands.Menus;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing menu-related operations in the application.
    /// </summary>
    public interface IMenuAppService
    {
        Task<int> CreateMenuAsync(CreateMenuCommand command);
        Task UpdateMenuAsync(UpdateMenuCommand command);
        Task DeleteMenuAsync(DeleteMenuCommand command);
        Task<IEnumerable<MenuDto>> GetAllMenusAsync();
        Task<MenuDto> GetMenuByIdAsync(int id);
    }
}