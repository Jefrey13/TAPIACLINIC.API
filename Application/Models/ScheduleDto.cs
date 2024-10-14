namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Schedule.
    /// </summary>
    public class ScheduleDto
    {
        /// <summary>
        /// ID of the Schedule.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the Staff member associated with the Schedule.
        /// </summary>
        public int StaffId { get; set; }

        /// <summary>
        /// Day of the week for the Schedule.
        /// </summary>
        public string DayOfWeek { get; set; }

        /// <summary>
        /// Start time of the Schedule.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// End time of the Schedule.
        /// </summary>
        public TimeSpan EndTime { get; set; }
    }
}