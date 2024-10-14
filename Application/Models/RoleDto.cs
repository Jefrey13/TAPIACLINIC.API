namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Role.
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// The unique identifier of the role.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the role.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if the role is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// List of permission IDs associated with the role.
        /// </summary>
        public List<int> PermissionIds { get; set; }

        /// <summary>
        /// List of menu IDs associated with the role.
        /// </summary>
        public List<int> MenuIds { get; set; }
    }
}