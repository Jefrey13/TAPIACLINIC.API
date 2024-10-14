using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Image
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ReferenceId { get; set; }  // Relación con `Exam` o `Surgery`

    [Required]
    [MaxLength(20)]
    public string Type { get; set; }  // "Exam" o "Surgery"

    [Required]
    public byte[] ImageData { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}