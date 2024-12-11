using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
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
        private readonly IMemoryCache _cache;
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context, IMemoryCache cache) : base(context) 
        {
            _cache = cache;
            _context = context;
        }

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
        /// Generates a unique PatientCode based on the user's initials and a global sequential number.
        /// Uses MemoryCache to store and manage the sequence.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <returns>A unique PatientCode.</returns>
        private async Task<string> GeneratePatientCodeAsync(string firstName, string lastName)
        {
            // Extract initials
            string initials = $"{firstName[0]}{lastName[0]}".ToUpper();

            // Retrieve the current sequence from the cache
            int nextSequence;
            if (!_cache.TryGetValue("PatientCodeSequence", out nextSequence))
            {
                // Load the last sequence from the database if not in cache
                var lastPatient = await _context.Users
                    .Where(u => u.RoleId == 4) // Only users with RoleId 4 (patients)
                    .OrderByDescending(u => u.PatientCode)
                    .FirstOrDefaultAsync();

                // Determine the next sequence number
                nextSequence = 1;
                if (lastPatient != null && !string.IsNullOrEmpty(lastPatient.PatientCode))
                {
                    var parts = lastPatient.PatientCode.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int lastSequence))
                    {
                        nextSequence = lastSequence + 1;
                    }
                }

                // Store the sequence in the cache
                _cache.Set("PatientCodeSequence", nextSequence);
            }

            // Increment and update the sequence in the cache
            _cache.Set("PatientCodeSequence", ++nextSequence);

            // Return the formatted PatientCode
            return $"{initials}-{nextSequence}";
        }

        /// <summary>
        /// Adds a new user to the database while handling unique constraint violations.
        /// This method ensures that database constraints for unique fields like UserName, Email, 
        /// and IdCard are respected by catching and interpreting database exceptions.
        /// If a unique constraint violation occurs, a detailed and user-friendly message is thrown.
        /// For other unexpected errors, a general exception is thrown with additional details.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a unique constraint violation occurs (e.g., duplicate UserName, Email, or IdCard)
        /// or when an unexpected database error happens.
        /// </exception>
        public override async Task AddAsync(User user)
        {
            try
            {
                // Serializar e imprimir el objeto recibido en la consola
                var serializedUser = System.Text.Json.JsonSerializer.Serialize(user);
                Console.WriteLine($"Adding user en el repositorio: {serializedUser}");

                // Generate PatientCode if the user is a patient (RoleId == 4)
                if (user.RoleId == 4) // Ensure this only applies to patients
                {
                    user.PatientCode = await GeneratePatientCodeAsync(user.FirstName, user.LastName);
                }

                user.StateId = 1;

                //await ValidateUniqueFieldsAsync(user);
                await base.AddAsync(user);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_User_UserName"))
                {
                    throw new InvalidOperationException($"El nombre de usuario '{user.UserName}' ya está en uso.");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_User_Email"))
                {
                    throw new InvalidOperationException($"El correo electrónico '{user.Email.Value}' ya está en uso.");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_User_IdCard"))
                {
                    throw new InvalidOperationException($"La cédula de identidad '{user.IdCard}' ya está registrada.");
                }
                throw new InvalidOperationException(
                    "Se produjo un error inesperado al guardar los datos del usuario. Por favor, intenta de nuevo o contacta al administrador del sistema.",
                    ex);
            }
        }

        /// <summary>
        /// Updates an existing user in the database while handling unique constraint violations.
        /// This method ensures that database constraints for unique fields like UserName, Email, 
        /// and IdCard are respected by catching and interpreting database exceptions.
        /// If a unique constraint violation occurs, a detailed and user-friendly message is thrown.
        /// For other unexpected errors, a general exception is thrown with additional details.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a unique constraint violation occurs (e.g., duplicate UserName, Email, or IdCard)
        /// or when an unexpected database error happens.
        /// </exception>
        public override async Task UpdateAsync(User user)
        {
            try
            {
                // Recuperar el usuario existente desde la base de datos
                var existingUser = await _context.Users.FindAsync(user.Id);

                if (existingUser == null)
                {
                    throw new Exception($"Usuario con ID {user.Id} no encontrado.");
                }

                // Actualizar únicamente los campos especificados
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Address = user.Address;
                existingUser.Gender = user.Gender;
                existingUser.BirthDate = user.BirthDate;
                existingUser.StateId = user.StateId;
                existingUser.ProfileImage = user.ProfileImage;
                existingUser.UpdatedAt = DateTime.Now;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Mostrar detalles del InnerException
                throw new Exception($"Error al actualizar el usuario: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Generic exception handler
                throw new Exception("Ocurrió un error inesperado. Por favor, contacta al administrador del sistema.", ex);
            }
        }





        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user with the specified email, or throws an exception if not found or invalid.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided email is null or empty.</exception>
        /// <exception cref="NotFoundException">Thrown when no user with the specified email is found.</exception>
        /// <exception cref="Exception">Thrown for any other errors during the retrieval process.</exception>
        public async Task<User> GetUserByEmailAsync(string email)
        {
                // Validate that the email is not null or empty
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException(nameof(email), "El correo electrónico no puede estar vacío o nulo.");
                }

                // Retrieve the user from the database by email
                var user = await _context.Users
                    .Include(u => u.State)  // Include the related State for reference
                    .Include(u => u.Role)   // Include the related Role for reference
                    .FirstOrDefaultAsync(u => u.Email.Value == email);

                return user;
        }

        /// <summary>
        /// Retrieves an active user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>The active user with the specified username, or null if not found or inactive.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided username is null or empty.</exception>
        /// <exception cref="Exception">Thrown when there is an issue retrieving the user from the database.</exception>
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            try
            {
                // Validate that the username is not null or empty
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new ArgumentNullException(nameof(userName), "El nombre de usuario no puede estar vacío o nulo.");
                }

                // Retrieve the user from the database if the state is 'Activo' (active)
                var user = await _context.Users
                    .Include(u => u.State) // Include the related State for reference
                    .Include(u => u.Role)  // Include the related Role for reference
                    .FirstOrDefaultAsync(u => u.UserName == userName && u.StateId == 1);

                return user;
            }
            catch (ArgumentNullException ex)
            {
                // Handle the case where the username is invalid (empty or null)
                throw new ArgumentException("El nombre de usuario proporcionado es inválido. Por favor, ingresa un nombre de usuario válido.");
            }
            catch (NotFoundException ex)
            {
                // Handle the case where no active user is found for the given username
                throw new Exception($"Lo sentimos, no encontramos al usuario con el nombre de usuario '{userName}'. {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling, providing a user-friendly message in Spanish
                throw new Exception("Ocurrió un problema al intentar obtener los datos del usuario. Por favor, inténtelo de nuevo más tarde.");
            }
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

        public async Task<bool> UpdateUserIsAccountActivated(string email)
        {
            User user = await GetUserByEmailAsync(email);

            if (user != null)
            {
                user.IsAccountActivated = true;
                

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task CreateAdminUserAsync()
        {
            // Verificar si ya existe un usuario administrador
            var existingAdmin = await _context.Users
                .FirstOrDefaultAsync(u => u.RoleId == 1);  // Admin roleId = 1

            if (existingAdmin != null)
            {
                return; // Ya existe un usuario administrador, no lo creamos de nuevo.
            }

            // Datos predeterminados del nuevo administrador
            string firstName = "Carlos";
            string lastName = "Gutiellez";
            string email = "carlos.torrente@example.com";
            string password = "Pa33Wor!";
            string phone = "+5058903971";
            string address = "Costado Sur Casa Las Mercedez";
            DateTime birthDate = new DateTime(2000, 1, 1);
            string idCard = "099-128998-9910W";
            string userName = "carlosadmin";
            int roleId = 1; // Asumiendo que el ID de administrador es 1

            // Encriptar la contraseña antes de insertarla
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Crear un nuevo usuario (User)
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = new Domain.ValueObjects.Email(email),  // Usando el Value Object Email
                Password = hashedPassword,
                Phone = new Domain.ValueObjects.PhoneNumber(phone),  // Usando el Value Object PhoneNumber
                Address = address,
                Gender = Domain.Enums.Gender.Male,  // Asumiendo género masculino
                BirthDate = birthDate,
                IdCard = idCard,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                StateId = 1,  // Estado activo
                RoleId = roleId,  // Admin role
                UserName = userName,
                HasAcceptedTermsAndConditions = true,
                IsAccountActivated = true,
                LastActivity = DateTime.Now
            };

            // Insertar el usuario en la base de datos
            await _context.Users.AddAsync(user);
        }
    }
}