namespace Application.Models.RequestDtos
{
    /// <summary>
    /// DTO representing the data for an Appointment.
    /// </summary>
    public class AppointmentRequestDto
    {
        /// <summary>
        /// The date range for the appointment.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The ID of the patient involved in the appointment.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// The ID of the staff member for the appointment.
        /// </summary>
        public int? StaffId { get; set; }

        /// <summary>
        /// The ID of the specialty associated with the appointment.
        /// </summary>
        public int SpecialtyId { get; set; }

        /// <summary>
        /// The ID of the schedule associated with the appointment.
        /// </summary>
        public int ScheduleId { get; set; }

        /// <summary>
        /// The ID of the state associated with the appointment.
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// The reason for the appointment.
        /// </summary>
        public string Reason { get; set; }

        public string? ChangeReason { get; set; }
    }
}