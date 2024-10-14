using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Consultation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RecordId { get; set; }
    [ForeignKey("RecordId")]
    public MedicalRecord MedicalRecord { get; set; }

    [Required]
    public int AppointmentId { get; set; }
    [ForeignKey("AppointmentId")]
    public Appointment Appointment { get; set; }

    [Required]
    public DateTime ConsultationDate { get; set; }

    [MaxLength(255)]
    public string Reason { get; set; }

    [MaxLength(255)]
    public string Diagnosis { get; set; }

    [MaxLength(255)]
    public string Treatment { get; set; }

    public DateTime? NextAppointmentDate { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<ConsultationExam> ConsultationExams { get; set; }

    public ICollection<RecordSurgery> RecordSurgeries { get; set; }
}