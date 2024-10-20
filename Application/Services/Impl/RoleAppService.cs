using Application.Commands.Roles;
using Application.Models;
using Application.Models.ReponseDtos;
using Application.Queries.Roles;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing roles in the system.
    /// This class interacts with MediatR to send commands and queries.
    /// </summary>
    public class RoleAppService : IRoleAppService
    {
        private readonly IMediator _mediator;

        public RoleAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new role using MediatR to send the creation command.
        /// </summary>
        /// <param name="command">Contains the role details in a DTO format.</param>
        /// <returns>The ID of the newly created role.</returns>
        public async Task<int> CreateRoleAsync(CreateRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Updates an existing role using MediatR to send the update command.
        /// Ensures that the provided role ID matches the command's role ID.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="command">Contains the updated role details.</param>
        public async Task UpdateRoleAsync(int id, UpdateRoleCommand command)
        {
            if (id != command.Id)
            {
                throw new ArgumentException("Role ID mismatch");
            }

            await _mediator.Send(command);
        }

        /// <summary>
        /// Deletes a role by its ID using MediatR to send the delete command.
        /// </summary>
        /// <param name="command">The command containing the role ID to delete.</param>
        public async Task DeleteRoleAsync(DeleteRoleCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Retrieves a role by its unique ID using MediatR to send a query.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>The DTO of the retrieved role.</returns>
        public async Task<RoleResponseDto> GetRoleByIdAsync(int id)
        {
            return await _mediator.Send(new GetRoleByIdQuery(id));
        }

        /// <summary>
        /// Retrieves all roles by sending a query through MediatR.
        /// </summary>
        /// <returns>A list of all roles as DTOs.</returns>
        public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
        {
            return await _mediator.Send(new GetAllRolesQuery());
        }
    }
}