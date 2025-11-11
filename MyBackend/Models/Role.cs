using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("Roles")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}