using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ReponseDtos
{
    public class StaffResponseDto
    {
        public int Id { get; set; }
        public string MinsaCode { get; set; }
        public int YearsExperience { get; set; }

        // Relación con el Usuario
        public UserResponseDto User { get; set; }  // Información completa del usuario

        // Relación con la Especialidad
        public int? SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }

        public DateTime HiringDate { get; set; }

        // Fechas
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
