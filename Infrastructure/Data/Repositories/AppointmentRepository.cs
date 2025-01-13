using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context) { }

    // Método para obtener citas por PatientId con las relaciones necesarias
    // Método para obtener citas con filtro por rol e id de usuario
    public async Task<IEnumerable<Appointment>> GetAllAsync(int? idRole = null, int id = 0)
    {
        var query = _context.Appointments
            .Include(a => a.Patient)                // Incluir el usuario paciente
            .Include(a => a.Staff)                  // Incluir el staff
            .ThenInclude(s => s.User)               // Incluir el usuario del staff
            .Include(a => a.Specialty)              // Incluir la especialidad
            .Include(a => a.Schedule)               // Incluir el horario
            .Include(a => a.State)                  // Incluir el estado
            .AsQueryable();

        // Filtro por rol
        if (idRole == 4) // Si el rol es igual a 4 (paciente)
        {
            query = query.Where(a => a.Patient.Id == id); // Filtrar citas por id del paciente
        }

        return await query.ToListAsync();
    }

    // Método para obtener una cita específica por Id con las relaciones necesarias
    public override async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)                // Incluir el usuario paciente
            .Include(a => a.Staff)                  // Incluir el staff
                .ThenInclude(s => s.User)           // Incluir el usuario del staff
            .Include(a => a.Specialty)              // Incluir la especialidad
            .Include(a => a.Schedule)               // Incluir el horario
            .Include(a => a.State)                  // Incluir el estado
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    // Método para obtener citas por PatientId con las relaciones necesarias
    public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
    {
        return await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .Include(a => a.Staff)                  // Incluir el staff
            .ThenInclude(s => s.User)           // Incluir el usuario del staff
            .Include(a => a.Specialty)              // Incluir la especialidad
            .Include(a => a.Schedule)               // Incluir el horario
            .Include(a => a.State)                  // Incluir el estado
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves appointments based on the specified state name.
    /// </summary>
    /// <param name="stateName">The name of the desired state (e.g., "Scheduled", "Completed").</param>
    /// <returns>A list of appointments with the specified state.</returns>
    public async Task<IEnumerable<Appointment>> GetByStateAsync(string stateName)
    {
        var stateId = await _context.States
            .Where(state => state.Name == stateName)
            .Select(state => state.Id)
            .FirstOrDefaultAsync();

        if (stateId == 0)
        {
            return Enumerable.Empty<Appointment>();
        }

        return await _context.Appointments
            .Where(appointment => appointment.StateId == stateId)
            .Include(a => a.Patient)    //Include related entities
            .Include(a => a.Staff)
            .Include(a => a.State)
            .Include(a => a.Specialty)
            .Include(a => a.Schedule)
            .AsNoTracking()
            .ToListAsync();
    }

    // Método para actualizar el estado de una cita usando el nombre del nuevo estado
    public async Task<bool> UpdateAppointmentStateAsync(int appointmentId, string StateName)
    {
        // Buscar el nuevo estado por nombre
        var newState = await _context.States.FirstOrDefaultAsync(s => s.Name == StateName);

        // Verificar si el estado existe
        if (newState == null)
        {
            return false; // Estado no encontrado
        }

        // Buscar la cita por ID
        var appointment = await _context.Appointments.FindAsync(appointmentId);

        // Verificar si la cita existe
        if (appointment == null)
        {
            return false; // Cita no encontrada
        }

        // Actualizar el estado de la cita
        appointment.StateId = newState.Id;

        // Guardar los cambios en la base de datos
        await _context.SaveChangesAsync();

        return true; // Éxito
    }
}