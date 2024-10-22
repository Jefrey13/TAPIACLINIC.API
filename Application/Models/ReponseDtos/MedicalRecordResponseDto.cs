using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ReponseDtos
{
    public class MedicalRecordResponseDto
    {
        public int Id { get; set; }
        public UserResponseDto Patient { get; set; }
        public StaffResponseDto Staff { get; set; }
        public DateTime OpeningDate { get; set; }
        public string Allergies { get; set; }
        public string PastIllnesses { get; set; }
        public string PastSurgeries { get; set; }
        public string FamilyHistory { get; set; }
        public int StateId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
