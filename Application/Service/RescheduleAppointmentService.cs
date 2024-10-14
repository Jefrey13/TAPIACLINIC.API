//public class RescheduleAppointmentService
//{
//    private readonly IAppointmentRepository _appointmentRepository;
//    private readonly IScheduleRepository _scheduleRepository;

//    public RescheduleAppointmentService(IAppointmentRepository appointmentRepository, IScheduleRepository scheduleRepository)
//    {
//        _appointmentRepository = appointmentRepository;
//        _scheduleRepository = scheduleRepository;
//    }

//    public void Reschedule(int appointmentId, DateRange newDateRange)
//    {
//        var appointment = _appointmentRepository.GetById(appointmentId);

//        if (appointment == null || !appointment.Active)
//        {
//            throw new InvalidOperationException("Appointment not found or is inactive.");
//        }

//        // Validate that the new time slot is available
//        if (!_scheduleRepository.IsTimeSlotAvailable(appointment.StaffId, newDateRange))
//        {
//            throw new InvalidOperationException("The new time slot is not available.");
//        }

//        // Update the appointment with the new date range
//        appointment.AppointmentDateRange = newDateRange;
//        appointment.UpdatedAt = DateTime.Now;

//        _appointmentRepository.UpdateAsync(appointment);
//    }
//}