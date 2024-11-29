using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDtos.UpdateRequestDto
{
    public class UserUpdateRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneAddress { get; set; }
        public string Gender { get; set; }  // "Male", "Female", etc.
        public DateTime BirthDate { get; set; }
        public int StateId { get; set; }  // Relación con el estado
        public int? RoleId { get; set; }   // Relación con el rol
    }
}
