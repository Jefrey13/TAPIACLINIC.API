using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for managing User entities.
    /// Handles CRUD operations and retrieval of users by specific criteria, such as email.
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.State)  // Incluir relación con el Estado
                .Include(u => u.Role)   // Incluir relación con el Rol
                .FirstOrDefaultAsync(u => u.Email.Value == email);
        }
        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                .Include(u => u.State)  // Incluir relación con el Estado
                .Include(u => u.Role)   // Incluir relación con el Rol
                .FirstOrDefaultAsync(u => u.UserName == userName);

        }

        /// <summary>
        /// Retrieves a user by their ID, including the related State and Role.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID, including the State and Role.</returns>
        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.State)  // Incluir relación con el Estado
                .Include(u => u.Role)   // Incluir relación con el Rol
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Retrieves all users, including the related State and Role.
        /// </summary>
        /// <returns>A list of users with their State and Role included.</returns>
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.State)
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task UpdatePasswordAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                existingUser.Password = user.Password;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves users who have the "Paciente" role and a specified state.
        /// </summary>
        /// <param name="stateId">The ID of the desired state (e.g., active, inactive).</param>
        /// <returns>A list of users with the "Paciente" role in the specified state.</returns>
        public async Task<IEnumerable<User>> GetByStateAsync(int stateId)
        {
            return await _context.Users
                .Where(user => user.StateId == stateId && user.Role.Name == "Paciente")
                .AsNoTracking()  // Optimize for read-only data by disabling tracking
                .ToListAsync();
        }
    }
}