using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for managing Staff entities, including relationships with User and Specialty.
    /// </summary>
    public class StaffRepository : BaseRepository<Staff>, IStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a staff member by their user ID, including the related User and Specialty.
        /// </summary>
        /// <param name="userId">The ID of the user associated with the staff member.</param>
        /// <returns>The staff member with the specified user ID.</returns>
        public async Task<Staff> GetStaffByUserIdAsync(int userId)
        {
            return await _context.Staffs
                .Include(s => s.User)  // Incluir la relación con el User
                .Include(s => s.Specialty)  // Incluir la relación con la Specialty
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        /// <summary>
        /// Retrieves all staff members associated with a specific specialty.
        /// </summary>
        /// <param name="specialtyId">The ID of the specialty.</param>
        /// <returns>A list of staff members associated with the specified specialty.</returns>
        public async Task<IEnumerable<Staff>> GetStaffBySpecialtyIdAsync(int specialtyId)
        {
            return await _context.Staffs
                .Include(s => s.User)  // Incluir la relación con el User
                .Include(s => s.Specialty)  // Incluir la relación con la Specialty
                .Where(s => s.SpecialtyId == specialtyId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all staff members, including related User and Specialty information.
        /// </summary>
        /// <returns>A list of all staff members with their associated User and Specialty information.</returns>
        public override async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staffs
                .Include(s => s.User)  // Incluir la relación con el User
                .Include(s => s.Specialty)  // Incluir la relación con la Specialty
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a staff member by their ID, including related User and Specialty information.
        /// </summary>
        /// <param name="id">The ID of the staff member.</param>
        /// <returns>The staff member with the specified ID.</returns>
        public override async Task<Staff> GetByIdAsync(int id)
        {
            return await _context.Staffs
                .Include(s => s.User)  // Incluir la relación con el User
                .Include(s => s.Specialty)  // Incluir la relación con la Specialty
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Adds a new staff member to the database.
        /// </summary>
        /// <param name="staff">The staff entity to add.</param>
        public override async Task AddAsync(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing staff member in the database.
        /// </summary>
        /// <param name="staff">The staff entity to update.</param>
        public override async Task UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a staff member from the database.
        /// </summary>
        /// <param name="staff">The staff entity to delete.</param>
        public override async Task DeleteAsync(Staff staff)
        {
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves staff members whose associated users have a specified state ID and a role other than "Paciente".
        /// </summary>
        /// <param name="stateId">The ID of the desired state (e.g., active, inactive).</param>
        /// <returns>A list of staff members with users in the specified state and a role other than "Paciente".</returns>
        public async Task<IEnumerable<Staff>> GetByStateAsync(int stateId)
        {
            return await _context.Staffs
                .Include(s => s.User)  // Include the associated User entity
                .Where(s => s.User.StateId == stateId && s.User.Role.Name != "Paciente")
                .AsNoTracking()  // Disable tracking for read-only data to improve performance
                .ToListAsync();
        }
    }
}