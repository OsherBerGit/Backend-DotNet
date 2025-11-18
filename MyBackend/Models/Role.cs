using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("Roles")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string Rolename { get; set; } = string.Empty;
    
    // Many-to-many back-reference
    // public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<User> Users { get; set; } = new List<User>();
}