//public class GenerateConsultationService
//{
//    private readonly IConsultationRepository _consultationRepository;

//    public GenerateConsultationService(IConsultationRepository consultationRepository)
//    {
//        _consultationRepository = consultationRepository;
//    }

//    public Consultation GenerateConsultation(int appointmentId, string diagnosis, string treatment)
//    {
//        // Create a new consultation based on the appointment
//        var consultation = new Consultation
//        {
//            AppointmentId = appointmentId,
//            Diagnosis = diagnosis,
//            Treatment = treatment,
//            ConsultationDate = DateTime.Now,
//            Active = true,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now
//        };

//        _consultationRepository.AddAsync(consultation);
//        return consultation;
//    }
//}