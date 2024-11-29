using Application.Commands.Staffs;
using Application.Models.ReponseDtos;
using Application.Queries.Staffs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for staff management operations in the application.
    /// </summary>
    public interface IStaffAppService
    {
        /// <summary>
        /// Creates a new staff member.
        /// </summary>
        /// <param name="command">The command containing staff creation data.</param>
        /// <returns>Returns true if the staff member was created successfully.</returns>
        Task<bool> CreateStaffAsync(CreateStaffCommand command);

        /// <summary>
        /// Updates an existing staff member.
        /// </summary>
        /// <param name="command">The command containing staff update data.</param>
        /// <returns>Returns true if the staff member was updated successfully.</returns>
        Task<bool> UpdateStaffAsync(UpdateStaffCommand command);

        /// <summary>
        /// Deletes a staff member by ID.
        /// </summary>
        /// <param name="command">The command containing the staff ID to delete.</param>
        /// <returns>Returns true if the staff member was deleted successfully.</returns>
        Task<bool> DeleteStaffAsync(DeleteStaffCommand command);

        /// <summary>
        /// Retrieves all staff members.
        /// </summary>
        /// <returns>A list of all staff members.</returns>
        Task<IEnumerable<StaffResponseDto>> GetAllStaffsAsync();

        /// <summary>
        /// Retrieves a staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member.</param>
        /// <returns>The staff member's data.</returns>
        Task<StaffResponseDto> GetStaffByIdAsync(int id);

        /// <summary>
        /// Retrieves a staff member by their associated User ID.
        /// </summary>
        /// <param name="userId">The User ID of the staff member.</param>
        /// <returns>The staff member's data.</returns>
        Task<StaffResponseDto> GetStaffByUserIdAsync(int userId);

        /// <summary>
        /// Retrieves staff members by their associated Specialty ID.
        /// </summary>
        /// <param name="specialtyId">The Specialty ID of the staff members.</param>
        /// <returns>A list of staff members in the given specialty.</returns>
        Task<IEnumerable<StaffResponseDto>> GetStaffBySpecialtyIdAsync(int specialtyId);

        /// <summary>
        /// Retrieves staff members by their associated state ID.
        /// </summary>
        /// <param name="stateId">The state ID to filter staff members.</param>
        /// <returns>A list of staff members in the specified state.</returns>
        Task<IEnumerable<StaffResponseDto>> GetStaffByStateAsync(int stateId);

        /// <summary>
        /// Retrieves staff members by role.
        /// </summary>
        /// <param name="roleName">The role name to filter staff members.</param>
        /// <returns>A list of staff members in the specified role.</returns>
        Task<IEnumerable<StaffResponseDto>> GetStaffByRoleAsync(string roleName);
    }
}