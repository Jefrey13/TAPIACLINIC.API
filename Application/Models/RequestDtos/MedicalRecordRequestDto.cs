namespace Application.Models.RequestDtos
{
    public class MedicalRecordRequestDto
    {
        public int PatientId { get; set; }
        public int? StaffId { get; set; }
        public DateTime? OpeningDate { get; set; } = DateTime.Now;
        public string? Allergies { get; set; }
        public string? PastIllnesses { get; set; }
        public string? PastSurgeries { get; set; }
        public string? FamilyHistory { get; set; }
        public int StateId { get; set; } 
        public string Diagnosis { get; set; }  // Diagnóstico
        public string Fundoscopy { get; set; }  // Fondo de Ojo
        public string Notes { get; set; }  // Notas
        public string Observations { get; set; }  // Observaciones
        public string Treatment { get; set; }  // Tratamiento

        public string ColorVision { get; set; }
        public string IntraocularPressureOD { get; set; }  // Presión Ocular OD
        public string IntraocularPressureOI { get; set; }  // Presión Ocular OI
        public string VisualAcuityOD { get; set; }  // Agudeza Visual OD
        public string VisualAcuityOI { get; set; }  // Agudeza Visual OI
    }
}
