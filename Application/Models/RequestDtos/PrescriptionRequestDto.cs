namespace Application.Models.RequestDtos
{
    public class PrescriptionRequestDto
    {
        public int PatientId { get; set; }  // ID of the patient
        public int DoctorId { get; set; }  // ID of the doctor
        public DateTime PrescriptionDate { get; set; }  // Date when the prescription was issued
        public string Diagnosis { get; set; }  // Diagnosis of the patient
        public string Treatment { get; set; }  // Prescribed treatment
        public string Notes { get; set; }  // Additional notes
        public string Observations { get; set; }  // Extra observations
    }
}