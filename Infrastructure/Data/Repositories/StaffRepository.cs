using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sprache;
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
        /// Validates if the UserName, Email, or IdCard of the associated User are already in use.
        /// </summary>
        /// <param name="user">The User entity to validate.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if any of the fields (UserName, Email, IdCard) are already in use.
        /// </exception>
        private async Task ValidateUniqueUserFieldsAsync(User user)
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

        public override async Task AddAsync(Staff staff)
        {
            try
            {
                // Verificar si el objeto Staff tiene un usuario asociado
                if (staff.User == null)
                {
                    throw new InvalidOperationException("La entidad de Personal debe tener una entidad de Usuario asociada.");
                }

                staff.User.StateId = 1;
                staff.User.IsAccountActivated = true;

                // Validar que los campos del usuario sean únicos (en caso de que exista validación de unicidad, como nombre de usuario o correo)
                await ValidateUniqueUserFieldsAsync(staff.User);

                // Logueamos la información antes de agregar el personal para depuración
                Console.WriteLine($"Agregando personal: {staff.User.UserName}, {staff.User.Email}");

                // Intentamos agregar la entidad Staff a la base de datos
                await base.AddAsync(staff);
            }
            catch (InvalidOperationException ex)
            {
                // Capturamos y manejamos excepciones específicas como InvalidOperationException
                Console.WriteLine($"Error de operación: {ex.Message}");
                throw new InvalidOperationException("Hubo un error en la operación. Verifique que todos los datos estén correctamente proporcionados.", ex);
            }
            catch (DbUpdateException dbEx)
            {
                // Capturamos excepciones relacionadas con la base de datos, como errores al guardar los cambios
                Console.WriteLine($"Error de base de datos: {dbEx.Message}");
                throw new DbUpdateException("Ocurrió un error al intentar guardar los cambios en la base de datos. Por favor, intente de nuevo.", dbEx);
            }
            catch (Exception ex)
            {
                // Capturamos cualquier otra excepción general
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
                throw new Exception("Ha ocurrido un error inesperado. Por favor, contacte con el soporte técnico.", ex);
            }
        }


        /// <summary>
        /// Updates an existing staff member in the database after validating associated User fields.
        /// </summary>
        /// <param name="staff">The staff entity to update.</param>
        public override async Task UpdateAsync(Staff staff)
        {
            try
            {
                // Verificar si el staff tiene un usuario asociado
                if (staff.User == null)
                    throw new InvalidOperationException("La entidad de Staff debe tener una entidad de Usuario asociada.");

                // Validar los campos únicos del usuario
                await ValidateUniqueUserFieldsAsync(staff.User);

                // Actualizar el staff en la base de datos
                await base.UpdateAsync(staff);
            }
            catch (InvalidOperationException ex)
            {
                // Manejo de excepciones con un mensaje claro en español
                throw new InvalidOperationException($"Error al intentar actualizar el personal: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones generales con un mensaje en español
                throw new Exception($"Error al actualizar el personal: {ex.Message}");
            }
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
            try
            {
                // Retrieve all staff members, including the related User and Specialty information
                var result = await _context.Staffs
                    .Include(s => s.User)  // Include the relation with the User
                    .Include(s => s.Specialty)  // Include the relation with the Specialty
                    .Include(s => s.User.Role)
                .Include(s => s.User.State)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // Friendly error message for the user in case of failure
                throw new Exception("Hubo un problema al intentar recuperar la lista de personal. Por favor, intente nuevamente más tarde.");
            }
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

        /// <summary>
        /// Retrieves staff members by role.
        /// </summary>
        /// <param name="roleName">The name of the desired rol (e.g., Personal Medico, Recepcionista).</param>
        /// <returns>A list of staff members with users in the specified role.</returns>
        public async Task<IEnumerable<Staff>> GetByRoleAsync(string roleName)
        {
            var result = await _context.Staffs
                .Include(s => s.User)
                .Include(s => s.User.Role)
                .Include(s => s.User.State)
                .Include(s => s.Specialty)
                .Where(s => s.User.Role.Name == roleName)
                .AsNoTracking()  // Disable tracking for read-only data to improve performance
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Toggles the active state of the staff entity's associated user.
        /// </summary>
        /// <param name="staffEntity">The staff entity whose associated user's state needs to be toggled.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided staff entity is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the staff entity is not associated with a valid user or the required states are missing in the States table.</exception>
        public override async Task ToggleActiveStateAsync(Staff staffEntity)
        {
            if (staffEntity == null)
                throw new ArgumentNullException(nameof(staffEntity));

            // Verify that the Staff entity has a valid relationship with a User
            if (staffEntity.User == null)
                throw new InvalidOperationException("The Staff entity is not associated with a User entity.");

            // Retrieve the "Activo" and "No Activo" states from the States table
            var activeState = await _context.States.FirstOrDefaultAsync(s => s.Name == "Activo");
            var inactiveState = await _context.States.FirstOrDefaultAsync(s => s.Name == "Inactivo");

            if (activeState == null || inactiveState == null)
            {
                throw new InvalidOperationException("The 'Activo' or 'Inactivo' states do not exist in the States table.");
            }

            // Toggle the StateId of the associated user
            var currentStateId = staffEntity.User.StateId;

            if (currentStateId == activeState.Id)
            {
                staffEntity.User.StateId = inactiveState.Id; // Change to "No Activo"
            }
            else if (currentStateId == inactiveState.Id)
            {
                staffEntity.User.StateId = activeState.Id; // Change to "Activo"
            }
            else
            {
                throw new InvalidOperationException("The current state is neither 'Activo' nor 'No Activo'.");
            }

            // Mark the User entity as modified so it can be updated in the database
            _context.Entry(staffEntity.User).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}