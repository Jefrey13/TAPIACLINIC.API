using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Staff
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public int? SpecialtyId { get; set; }
    [ForeignKey("SpecialtyId")]
    public Specialty Specialty { get; set; }

    [MaxLength(50)]
    public string? MinsaCode { get; set; }

    public int YearsExperience { get; set; }

    public DateTime HiringDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<SurgeryStaff> SurgeryStaffs { get; set; }
}