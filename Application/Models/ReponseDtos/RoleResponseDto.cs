using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ReponseDtos
{
    /// <summary>
    /// DTO representing the response data for a Role.
    /// </summary>
    public class RoleResponseDto
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
        /// List of permissions associated with the role.
        /// </summary>
        public List<PermissionDto> Permissions { get; set; }

        /// <summary>
        /// List of menus associated with the role.
        /// </summary>
        public List<MenuDto> Menus { get; set; }
    }
}
