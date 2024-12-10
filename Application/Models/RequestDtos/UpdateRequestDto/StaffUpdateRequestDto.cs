using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDtos.UpdateRequestDto
{
    public class StaffUpdateRequestDto
    {
        public UserUpdateRequestDto User { get; set; }  // Datos del usuario a actualizar
        public int? SpecialtyId { get; set; }
        public int? YearsExperience { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
