using Application.Models.ReponseDtos;

namespace Application.Models.ResponseDtos
{
    /// <summary>
    /// DTO for responding with appointment details, including related entities.
    /// </summary>
    public class AppointmentResponseDto
    {
        /// <summary>
        /// The unique identifier of the appointment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date range for the appointment.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The patient details associated with the appointment.
        /// </summary>
        public UserResponseDto Patient { get; set; }

        /// <summary>
        /// The staff member details associated with the appointment.
        /// </summary>
        public StaffResponseDto Staff { get; set; }

        /// <summary>
        /// The specialty details associated with the appointment.
        /// </summary>
        public SpecialtyDto Specialty { get; set; }

        /// <summary>
        /// The schedule details associated with the appointment.
        /// </summary>
        public ScheduleResponseDto Schedule { get; set; }

        /// <summary>
        /// The state details associated with the appointment.
        /// </summary>
        public StateDto State { get; set; }

        /// <summary>
        /// The reason for the appointment.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Indicates whether the appointment is active.
        /// </summary>
        public bool Active { get; set; }
    }
}