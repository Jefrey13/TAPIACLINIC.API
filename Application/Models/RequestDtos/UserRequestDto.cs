using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDtos
{
    /// <summary>
    /// DTO representing the data for creating or updating a user.
    /// </summary>
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }  // Hashed password
        public string PhoneAddress { get; set; }
        public string Gender { get; set; }  // "Male", "Female", etc.
        public DateTime BirthDate { get; set; }
        public string IdCard { get; set; }  // Número de identificación
        public int StateId { get; set; }  // Relación con el estado
        public int RoleId { get; set; }   // Relación con el rol
    }
}
