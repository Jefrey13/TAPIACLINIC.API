using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ReponseDtos
{
    /// <summary>
    /// DTO representing the response data for a User.
    /// </summary>
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneAddress { get; set; }
        public string Gender { get; set; }  // "Male", "Female", etc.
        public DateTime BirthDate { get; set; }
        public string IdCard { get; set; }  // Número de identificación

        // Estado (State)
        public int StateId { get; set; }  // ID del estado
        public string State { get; set; }  // Nombre del estado

        // Rol (Role)
        public int RoleId { get; set; }  // ID del rol
        public string Role { get; set; }   // Nombre del rol

        public bool IsActive { get; set; }

        public DateTime? LastActivity { get; set; }
    }
}
