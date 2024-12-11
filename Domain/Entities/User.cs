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

    //[Required]
    public string? PatientCode { get; set; }

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

    public DateTime? LastActivity { get; set; }

    //This property is not necessary because the application already have uses the State table, to manage active and inactive statuses.
    //public bool IsUsernameAvailable { get; set; } = true;


    //Opcion para validar si la cuenta esta activa o si no lo esta aun.


    // Indicates whether the user's account is activated.
    public bool? IsAccountActivated { get; set; }


    // Indicates whether the user has accepted the terms and conditions during login.

    public bool? HasAcceptedTermsAndConditions { get; set; } = true;

    public byte[]? ProfileImage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int StateId { get; set; }
    [ForeignKey("StateId")]
    public State State { get; set; }

    public int? RoleId { get; set; }
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}