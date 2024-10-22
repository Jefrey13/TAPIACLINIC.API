using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDtos
{
    public class MedicalRecordRequestDto
    {
        public int PatientId { get; set; }
        public int StaffId { get; set; }
        public DateTime OpeningDate { get; set; }
        public string Allergies { get; set; }
        public string PastIllnesses { get; set; }
        public string PastSurgeries { get; set; }
        public string FamilyHistory { get; set; }
        public int StateId { get; set; }
    }
}
