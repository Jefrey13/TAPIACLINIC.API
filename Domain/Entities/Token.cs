using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Token
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required]
    [MaxLength(255)]
    public string TokenValue { get; set; }

    [Required]
    [MaxLength(20)]
    public string TokenType { get; set; }  // 'Session' o 'Password_Change'

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime ExpirationDate { get; set; }
}