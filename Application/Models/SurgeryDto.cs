namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Surgery.
    /// </summary>
    public class SurgeryDto
    {
        /// <summary>
        /// ID of the Surgery.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Surgery.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the Surgery.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ID representing the state of the Surgery.
        /// </summary>
        public int StateId { get; set; }
    }
}