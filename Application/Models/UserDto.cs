namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a User.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gender of the user.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Birth date of the user.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Identification card number of the user.
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// Whether the user is active or not.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The date and time the user was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time the user was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Role identifier for the user.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// State identifier for the user.
        /// </summary>
        public int StateId { get; set; }
    }
}