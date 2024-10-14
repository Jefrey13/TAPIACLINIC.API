using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class SurgeryStaff
{
    [Key]
    [Column(Order = 0)]
    public int SurgeryId { get; set; }
    [ForeignKey("SurgeryId")]
    public Surgery Surgery { get; set; }

    [Key]
    [Column(Order = 1)]
    public int StaffId { get; set; }
    [ForeignKey("StaffId")]
    public Staff Staff { get; set; }

    [MaxLength(50)]
    public string StaffRole { get; set; }

    public TimeSpan? ParticipationDuration { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}