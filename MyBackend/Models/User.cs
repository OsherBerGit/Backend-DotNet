using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    // Many-to-many relationship with Role table
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}