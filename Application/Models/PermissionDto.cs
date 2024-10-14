namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Permission.
    /// </summary>
    public class PermissionDto
    {
        /// <summary>
        /// The unique identifier of the permission.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the permission.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the permission.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if the permission is active.
        /// </summary>
        public bool Active { get; set; }
    }
}