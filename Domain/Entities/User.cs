using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    public Email Email { get; set; }  // Usando Email como un Value Object

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    [Required]
    public PhoneNumber Phone { get; set; }  // Usando PhoneNumber como un Value Object

    [MaxLength(255)]
    public string Address { get; set; }

    [Required]
    public Gender Gender { get; set; }  // Usando el enum Gender

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [MaxLength(20)]
    public string IdCard { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int StateId { get; set; }
    [ForeignKey("StateId")]
    public State State { get; set; }

    public int? RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}