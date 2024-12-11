using Application.Exceptions;
using Application.Models;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Menus
{
    /// <summary>
    /// Handler for retrieving menus based on the role ID.
    /// </summary>
    public class GetMenusByRoleIdQueryHandler : IRequestHandler<GetMenusByRoleIdQuery, IEnumerable<MenuResponseDto>>
    {
        private readonly IMenuRepository _menuRepository; // Repository to fetch menu data
        private readonly IMapper _mapper; // Mapper to convert entities to DTOs

        /// <summary>
        /// Initializes a new instance of the handler with the menu repository and the mapper.
        /// </summary>
        /// <param name="menuRepository">The repository used to fetch menus.</param>
        /// <param name="mapper">The AutoMapper instance to map data between entities and DTOs.</param>
        public GetMenusByRoleIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository), "Menu repository cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null.");
        }

        /// <summary>
        /// Handles the query to retrieve menus based on the provided role ID.
        /// </summary>
        /// <param name="request">The query request containing the role ID.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A list of MenuResponseDto containing the menu details.</returns>
        /// <exception cref="NotFoundException">Thrown when no menus are found for the provided role ID.</exception>
        /// <exception cref="Exception">Thrown for general errors.</exception>
        public async Task<IEnumerable<MenuResponseDto>> Handle(GetMenusByRoleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve menus based on the RoleId provided in the request
                var menus = await _menuRepository.GetMenusByRoleIdAsync(request.RoleId);

                // Check if no menus are found and throw a custom exception with a user-friendly message
                if (menus == null || !menus.Any())
                {
                    throw new NotFoundException("Menús no encontrados", $"No se encontraron menús para el rol con ID {request.RoleId}. Por favor, verifica el ID de rol e inténtalo nuevamente.");
                }

                // Map the retrieved menus to the MenuResponseDto
                return _mapper.Map<IEnumerable<MenuResponseDto>>(menus);
            }
            catch (NotFoundException ex)
            {
                // Handle the case where no menus are found and return a meaningful message for the user
                throw new Exception($"Lo siento, no pudimos encontrar los menús. {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling with a more friendly message in Spanish for the user
                throw new Exception("Ocurrió un problema al intentar obtener los menús. Por favor, inténtelo de nuevo más tarde.");
            }
        }
    }
}