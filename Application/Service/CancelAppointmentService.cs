//public class CancelAppointmentService
//{
//    private readonly IAppointmentRepository _appointmentRepository;

//    public CancelAppointmentService(IAppointmentRepository appointmentRepository)
//    {
//        _appointmentRepository = appointmentRepository;
//    }

//    public void Cancel(int appointmentId)
//    {
//        var appointment = _appointmentRepository.GetById(appointmentId);
//        if (appointment == null || !appointment.Active)
//        {
//            throw new InvalidOperationException("Appointment not found or is inactive.");
//        }

//        // Mark the appointment as inactive (canceled)
//        appointment.Active = false;
//        appointment.UpdatedAt = DateTime.Now;

//        _appointmentRepository.UpdateAsync(appointment);
//    }
//}