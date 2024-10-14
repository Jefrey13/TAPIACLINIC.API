using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Specialty
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Staff> Staffs { get; set; }
}