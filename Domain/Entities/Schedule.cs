using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Schedule
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StaffId { get; set; }
    [ForeignKey("StaffId")]
    public Staff Staff { get; set; }

    [Required]
    [MaxLength(10)]
    public string DayOfWeek { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}