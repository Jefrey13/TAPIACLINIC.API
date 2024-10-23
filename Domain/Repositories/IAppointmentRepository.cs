
using Domain.Entities;

namespace Domain.Repositories;
public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
    //Task<IEnumerable<Appointment>> GetAppointmentsByStaffIdAsync(int staffId);
}