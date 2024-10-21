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
                .Include(u => u.State)  // Incluir relación con el Estado
                .Include(u => u.Role)   // Incluir relación con el Rol
                .ToListAsync();
        }
    }
}