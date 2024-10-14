//public class ConfirmAppointmentService
//{
//    private readonly IAppointmentRepository _appointmentRepository;
//    private readonly IConsultationRepository _consultationRepository;

//    public ConfirmAppointmentService(IAppointmentRepository appointmentRepository, IConsultationRepository consultationRepository)
//    {
//        _appointmentRepository = appointmentRepository;
//        _consultationRepository = consultationRepository;
//    }

//    public void Confirm(int appointmentId)
//    {
//        var appointment = _appointmentRepository.GetById(appointmentId);
//        if (appointment == null || !appointment.Active)
//        {
//            throw new InvalidOperationException("Appointment not found or is inactive.");
//        }

//        // Mark the appointment as confirmed
//        appointment.StateId = (int)AppointmentState.Confirmed;
//        appointment.UpdatedAt = DateTime.Now;
//        _appointmentRepository.UpdateAsync(appointment);

//        // Generate a medical consultation after confirming the appointment
//        var consultation = new Consultation
//        {
//            AppointmentId = appointment.Id,
//            RecordId = appointment.PatientId, // Assuming the patient has a RecordId
//            ConsultationDate = DateTime.Now,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now,
//            Active = true
//        };

//        _consultationRepository.Add(consultation);
//    }
//}