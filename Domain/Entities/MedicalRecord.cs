using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MedicalRecord
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }
    [ForeignKey("PatientId")]
    public User Patient { get; set; }

    [Required]
    public int StaffId { get; set; }
    [ForeignKey("StaffId")]
    public Staff Staff { get; set; }

    [Required]
    public DateTime OpeningDate { get; set; }

    [MaxLength(255)]
    public string? Allergies { get; set; }

    [MaxLength(255)]
    public string? PastIllnesses { get; set; }

    [MaxLength(255)]
    public string? PastSurgeries { get; set; }

    [MaxLength(255)]
    public string? FamilyHistory { get; set; }

    [Required]
    public int StateId { get; set; }
    [ForeignKey("StateId")]
    public State State { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [MaxLength(255)]
    public string Diagnosis { get; set; }

    [MaxLength(255)]
    public string Fundoscopy { get; set; }

    [MaxLength(255)]
    public string ColorVision { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; }

    [MaxLength(500)]
    public string Observations { get; set; }

    [MaxLength(255)]
    public string Treatment { get; set; }

    public string IntraocularPressureOD { get; set; }

    public string IntraocularPressureOI { get; set; }

    public string VisualAcuityOD { get; set; }

    public string VisualAcuityOI { get; set; }

    public ICollection<RecordSurgery>? RecordSurgeries { get; set; }
}