
using Domain.Entities;

namespace Domain.Repositories;
public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
    //Task<IEnumerable<Appointment>> GetAppointmentsByStaffIdAsync(int staffId);

    Task<IEnumerable<Appointment>> GetByStateAsync(string stateName);

    Task<bool> UpdateAppointmentStateAsync(int appointmentId, string StateName);

    // Implementación específica para GetAllAsync con idRole
    Task<IEnumerable<Appointment>> GetAllAsync(int? idRole = null, int id = 0);
}