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
        /// Validates if the UserName, Email, or IdCard are already in use.
        /// </summary>
        /// <param name="user">The user entity to validate.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if any of the fields (UserName, Email, IdCard) are already in use.
        /// </exception>
        private async Task ValidateUniqueFieldsAsync(User user)
        {
            var existingUser = await _context.Users
                .Where(u => u.Id != user.Id) // Exclude the current user for updates
                .Where(u => u.UserName == user.UserName
                    || u.Email.Value == user.Email.Value
                    || u.IdCard == user.IdCard)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                if (existingUser.UserName == user.UserName)
                    throw new InvalidOperationException($"El nombre de usuario '{user.UserName}' ya está en uso. Por favor, elija otro.");
                if (existingUser.Email.Value == user.Email.Value)
                    throw new InvalidOperationException($"El correo electrónico '{user.Email.Value}' ya está en uso. Por favor, use otro.");
                if (existingUser.IdCard == user.IdCard)
                    throw new InvalidOperationException($"La cédula de identidad '{user.IdCard}' ya está registrada. Verifique los datos ingresados.");

            }
        }

        /// <summary>
        /// Adds a new user to the database after validating unique fields.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        public override async Task AddAsync(User user)
        {
            await ValidateUniqueFieldsAsync(user);
            await base.AddAsync(user);
        }

        /// <summary>
        /// Updates an existing user in the database after validating unique fields.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        public override async Task UpdateAsync(User user)
        {
            await ValidateUniqueFieldsAsync(user);
            await base.UpdateAsync(user);
        }

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
        /// Retrieves an active user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>The active user with the specified username, or null if not found or inactive.</returns>
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            // Retrieve the user only if they are in the "Activo" state
            return await _context.Users
                .Include(u => u.State) // Include the related State
                .Include(u => u.Role)  // Include the related Role
                .FirstOrDefaultAsync(u => u.UserName == userName && u.StateId == 1);
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
                .Include(user => user.State)  // Incluir el estado relacionado
                .Include(user => user.Role)   // Incluir el rol relacionado
                .Where(user => user.StateId == stateId && user.Role.Name == "Paciente")
                .AsNoTracking()               // Optimizar para datos de solo lectura
                .ToListAsync();
        }

    }
}