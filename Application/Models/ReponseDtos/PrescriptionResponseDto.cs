using Application.Models.ReponseDtos;

namespace Application.Models.ResponseDtos
{
    public class PrescriptionResponseDto
    {
        public int Id { get; set; }
        public UserResponseDto Patient { get; set; }  // Contains patient's details
        public StaffResponseDto Doctor { get; set; }  // Contains doctor's details
        public DateTime PrescriptionDate { get; set; }  // Date when the prescription was issued
        public string Diagnosis { get; set; }  // Diagnosis of the patient
        public string Treatment { get; set; }  // Prescribed treatment
        public string Notes { get; set; }  // Additional notes
        public string Observations { get; set; }  // Extra observations
        public DateTime CreatedAt { get; set; }  // When the prescription was created
        public DateTime UpdatedAt { get; set; }  // When the prescription was last updated
    }
}