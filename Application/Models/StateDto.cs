namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a State.
    /// </summary>
    public class StateDto
    {
        /// <summary>
        /// The unique identifier of the state.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the state.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// The type of the state.
        /// </summary>
        public string StateType { get; set; }

        /// <summary>
        /// A brief description of the state.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the state is active or not.
        /// </summary>
        public bool Active { get; set; }
    }
}