using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<RoleMenu> RoleMenus { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }
}