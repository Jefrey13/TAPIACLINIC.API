using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class RecordSurgery
{
    [Key]
    [Column(Order = 0)]
    public int RecordId { get; set; }
    [ForeignKey("RecordId")]
    public MedicalRecord MedicalRecord { get; set; }

    [Key]
    [Column(Order = 1)]
    public int SurgeryId { get; set; }
    [ForeignKey("SurgeryId")]
    public Surgery Surgery { get; set; }

    public int? ConsultationId { get; set; }
    [ForeignKey("ConsultationId")]
    public Consultation Consultation { get; set; }

    [Required]
    public SurgeryType SurgeryType { get; set; }  // Usando el enum SurgeryType

    [Required]
    public DateRange SurgerySchedule { get; set; }  // Usando DateRange para manejar la fecha de cirugía

    public TimeSpan? Duration { get; set; }

    [MaxLength(255)]
    public string Complications { get; set; }

    [MaxLength(255)]
    public string Result { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}