//public class ScheduleAppointmentService
//{
//    private readonly IAppointmentRepository _appointmentRepository;
//    private readonly IScheduleRepository _scheduleRepository;

//    public ScheduleAppointmentService(IAppointmentRepository appointmentRepository, IScheduleRepository scheduleRepository)
//    {
//        _appointmentRepository = appointmentRepository;
//        _scheduleRepository = scheduleRepository;
//    }

//    public Appointment Schedule(int patientId, int staffId, int specialtyId, DateRange appointmentDateRange, string reason)
//    {
//        // Validate that the patient doesn't have another appointment on the same day
//        if (_appointmentRepository.HasAppointmentOnSameDay(patientId, appointmentDateRange.Start))
//        {
//            throw new InvalidOperationException("The patient already has an appointment scheduled for that day.");
//        }

//        // Validate that the selected time slot is available
//        if (!_scheduleRepository.IsTimeSlotAvailable(staffId, appointmentDateRange))
//        {
//            throw new InvalidOperationException("The time slot is not available.");
//        }

//        // Create the new appointment
//        var appointment = new Appointment
//        {
//            PatientId = patientId,
//            StaffId = staffId,
//            SpecialtyId = specialtyId,
//            AppointmentDateRange = appointmentDateRange,
//            Reason = reason,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now,
//            Active = true
//        };

//        _appointmentRepository.AddAsync(appointment);
//        return appointment;
//    }
//}