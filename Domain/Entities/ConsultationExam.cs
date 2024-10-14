using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class ConsultationExam
{
    [Key]
    [Column(Order = 0)]
    public int ConsultationId { get; set; }
    [ForeignKey("ConsultationId")]
    public Consultation Consultation { get; set; }

    [Key]
    [Column(Order = 1)]
    public int ExamId { get; set; }
    [ForeignKey("ExamId")]
    public Exam Exam { get; set; }

    [MaxLength]
    public string Result { get; set; }

    [Required]
    public DateTime ExamDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}