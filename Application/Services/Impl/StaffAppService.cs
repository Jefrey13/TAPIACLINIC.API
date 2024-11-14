using Application.Commands.Staffs;
using Application.Models.ReponseDtos;
using Application.Queries.Staffs;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing staff members.
    /// Uses MediatR to send commands and queries.
    /// </summary>
    public class StaffAppService : IStaffAppService
    {
        private readonly IMediator _mediator;

        public StaffAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sends a command to create a new staff member.
        /// </summary>
        /// <param name="command">The command containing staff creation data.</param>
        /// <returns>The ID of the newly created staff member.</returns>
        public async Task<int> CreateStaffAsync(CreateStaffCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to update an existing staff member.
        /// </summary>
        /// <param name="command">The command containing staff update data.</param>
        public async Task UpdateStaffAsync(UpdateStaffCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to delete a staff member.
        /// </summary>
        /// <param name="command">The command containing the staff ID to delete.</param>
        public async Task DeleteStaffAsync(DeleteStaffCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a query to retrieve all staff members.
        /// </summary>
        /// <returns>A list of all staff members.</returns>
        public async Task<IEnumerable<StaffResponseDto>> GetAllStaffsAsync()
        {
            return await _mediator.Send(new GetAllStaffsQuery());
        }

        /// <summary>
        /// Sends a query to retrieve a staff member by ID.
        /// </summary>
        /// <param name="id">The ID of the staff member.</param>
        /// <returns>The staff member's data.</returns>
        public async Task<StaffResponseDto> GetStaffByIdAsync(int id)
        {
            return await _mediator.Send(new GetStaffByIdQuery(id));
        }

        /// <summary>
        /// Sends a query to retrieve a staff member by User ID.
        /// </summary>
        /// <param name="userId">The User ID of the staff member.</param>
        /// <returns>The staff member's data.</returns>
        public async Task<StaffResponseDto> GetStaffByUserIdAsync(int userId)
        {
            return await _mediator.Send(new GetStaffByUserIdQuery(userId));
        }

        /// <summary>
        /// Sends a query to retrieve staff members by Specialty ID.
        /// </summary>
        /// <param name="specialtyId">The Specialty ID of the staff members.</param>
        /// <returns>A list of staff members in the given specialty.</returns>
        public async Task<IEnumerable<StaffResponseDto>> GetStaffBySpecialtyIdAsync(int specialtyId)
        {
            return await _mediator.Send(new GetStaffBySpecialtyIdQuery(specialtyId));
        }

        /// <summary>
        /// Sends a query to retrieve staff members by State ID.
        /// </summary>
        /// <param name="stateId">The State ID to filter staff members.</param>
        /// <returns>A list of staff members in the specified state.</returns>
        public async Task<IEnumerable<StaffResponseDto>> GetStaffByStateAsync(int stateId)
        {
            return await _mediator.Send(new GetByStateQuery(stateId));
        }
    }
}