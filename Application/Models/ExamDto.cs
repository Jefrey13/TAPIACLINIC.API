namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for an Exam.
    /// </summary>
    public class ExamDto
    {
        /// <summary>
        /// The unique identifier of the exam.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the exam.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the exam.
        /// </summary>
        public string Description { get; set; }
    }
}