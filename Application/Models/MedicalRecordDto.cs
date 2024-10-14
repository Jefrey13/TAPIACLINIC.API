namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Medical Record.
    /// </summary>
    public class MedicalRecordDto
    {
        /// <summary>
        /// The unique identifier of the medical record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the patient associated with this medical record.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// The name of the patient.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// The ID of the staff who manages this medical record.
        /// </summary>
        public int StaffId { get; set; }

        /// <summary>
        /// The name of the staff (doctor) managing the medical record.
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// The opening date of the medical record.
        /// </summary>
        public DateTime OpeningDate { get; set; }

        /// <summary>
        /// The list of allergies for the patient.
        /// </summary>
        public string Allergies { get; set; }

        /// <summary>
        /// The past illnesses of the patient.
        /// </summary>
        public string PastIllnesses { get; set; }

        /// <summary>
        /// Any past surgeries the patient had.
        /// </summary>
        public string PastSurgeries { get; set; }

        /// <summary>
        /// Family medical history of the patient.
        /// </summary>
        public string FamilyHistory { get; set; }

        /// <summary>
        /// The state ID of the medical record (e.g., active, archived).
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Whether the record is currently active.
        /// </summary>
        public bool Active { get; set; }
    }
}