namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for an Appointment.
    /// </summary>
    public class AppointmentDto
    {
        /// <summary>
        /// The unique identifier of the appointment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date range for the appointment.
        /// </summary>
        public DateRangeDto AppointmentDateRange { get; set; }

        /// <summary>
        /// The ID of the patient involved in the appointment.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// The ID of the staff member for the appointment.
        /// </summary>
        public int StaffId { get; set; }

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

        /// <summary>
        /// Indicates whether the appointment is active.
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// DTO representing a date range for an appointment.
    /// </summary>
    public class DateRangeDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}