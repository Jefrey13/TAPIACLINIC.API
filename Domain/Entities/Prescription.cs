using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PrescriptionDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Diagnosis { get; set; }

        [Required]
        [MaxLength(500)]
        public string Treatment { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        [MaxLength(500)]
        public string Observations { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public User Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Staff Doctor { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}