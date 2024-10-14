//public class RegisterDiagnosisService
//{
//    private readonly IConsultationRepository _consultationRepository;

//    public RegisterDiagnosisService(IConsultationRepository consultationRepository)
//    {
//        _consultationRepository = consultationRepository;
//    }

//    public void RegisterDiagnosis(int consultationId, string diagnosis, string treatment)
//    {
//        var consultation = _consultationRepository.GetById(consultationId);
//        if (consultation == null || !consultation.Active)
//        {
//            throw new InvalidOperationException("Consultation not found or is inactive.");
//        }

//        // Update the diagnosis and treatment
//        consultation.Diagnosis = diagnosis;
//        consultation.Treatment = treatment;
//        consultation.UpdatedAt = DateTime.Now;

//        _consultationRepository.UpdateAsync(consultation);
//    }
//}