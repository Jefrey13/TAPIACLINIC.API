using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext context) : base(context) { }

    // Método para obtener citas por PatientId con las relaciones necesarias
    public override async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)                // Incluir el usuario paciente
            .Include(a => a.Staff)                  // Incluir el staff
                .ThenInclude(s => s.User)           // Incluir el usuario del staff
            .Include(a => a.Specialty)              // Incluir la especialidad
            .Include(a => a.Schedule)               // Incluir el horario
            .Include(a => a.State)                  // Incluir el estado
            .ToListAsync();
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
            .Include(a => a.Specialty)
            .Include(a => a.Schedule)
            .AsNoTracking()
            .ToListAsync();
    }
}