using Application.Commands.Menus;
using Application.Models;
using Application.Models.ReponseDtos;
using Application.Queries.Menus;
using Application.Queries.Roles;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing menus.
    /// This class uses MediatR for handling commands and queries, and AutoMapper for entity-to-DTO mapping.
    /// </summary>
    public class MenuAppService : IMenuAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;

        public MenuAppService(IMediator mediator, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<int> CreateMenuAsync(CreateMenuCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateMenuAsync(UpdateMenuCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteMenuAsync(DeleteMenuCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<MenuDto>> GetAllMenusAsync()
        {
            return await _mediator.Send(new GetAllMenusQuery());
        }

        public async Task<MenuDto> GetMenuByIdAsync(int id)
        {
            return await _mediator.Send(new GetMenuByIdQuery(id));
        }
        public async Task<IEnumerable<MenuResponseDto>> GetMenusByRoleAsync(string jwtToken)
        {
            // Extraemos los roles del JWT
            var role = _jwtTokenService.GetRolesFromToken(jwtToken);

            if (role == null || !role.Any())
            {
                throw new Exception("Roles not found in JWT.");
            }

            // Obtenemos el RoleId basado en el nombre del rol
            var roleId = await _mediator.Send(new GetRoleIdByNameQuery(role.First().ToString()));

            if (roleId == null)
            {
                throw new Exception($"Role with name '{role.First().ToString()}' not found.");
            }

            // Enviamos la consulta para obtener los menús relacionados con el roleId y el menu Id
            return await _mediator.Send(new GetMenusByRoleIdQuery(roleId.Value));
        }
    }
}