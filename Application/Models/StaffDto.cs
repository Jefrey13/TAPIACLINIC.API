namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Staff.
    /// </summary>
    public class StaffDto
    {
        /// <summary>
        /// The unique identifier of the staff.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user details associated with the staff.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// The specialty ID associated with the staff.
        /// </summary>
        public int? SpecialtyId { get; set; }

        /// <summary>
        /// MINSA code for the staff.
        /// </summary>
        public string MinsaCode { get; set; }

        /// <summary>
        /// Years of experience for the staff.
        /// </summary>
        public int? YearsExperience { get; set; }

        /// <summary>
        /// Indicates whether the staff is active.
        /// </summary>
        public bool Active { get; set; }
    }
}