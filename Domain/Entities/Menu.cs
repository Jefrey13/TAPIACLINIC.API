using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Menu
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string Url { get; set; }

    [MaxLength(100)]
    public string Icon { get; set; }

    public int? ParentId { get; set; }
    [ForeignKey("ParentId")]
    public Menu Parent { get; set; }

    public int Order { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Relación many-to-many con Roles
    public ICollection<Role> Roles { get; set; }

    public ICollection<RoleMenu> RoleMenus { get; set; }

    // Nueva propiedad para los submenús
    public ICollection<Menu> Children { get; set; } = new List<Menu>();
}