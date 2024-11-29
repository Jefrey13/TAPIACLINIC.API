using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDtos
{
    public class StaffRequestDto
    {
        public UserRequestDto User { get; set; }  // Datos del usuario
        public int? SpecialtyId { get; set; }
        public string? MinsaCode { get; set; }
        public int? YearsExperience { get; set; }
        public bool Active { get; set; }
    }
}
