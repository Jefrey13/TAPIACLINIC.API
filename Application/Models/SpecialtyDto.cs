namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Specialty.
    /// </summary>
    public class SpecialtyDto
    {
        /// <summary>
        /// ID of the Specialty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Specialty.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the Specialty.
        /// </summary>
        public string Description { get; set; }
    }
}
