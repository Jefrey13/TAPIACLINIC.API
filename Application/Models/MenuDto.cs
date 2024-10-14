namespace Application.Models
{
    /// <summary>
    /// DTO representing the data for a Menu.
    /// </summary>
    public class MenuDto
    {
        /// <summary>
        /// The unique identifier of the menu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the menu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL associated with the menu.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Icon for the menu.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The order in which the menu appears.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Indicates if the menu is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// ID of the parent menu, if any.
        /// </summary>
        public int? ParentId { get; set; }
    }
}