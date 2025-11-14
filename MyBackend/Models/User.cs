using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("Users")]
public class User
{
    [Key] // Primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
    public int Id { get; set; }

    [Required(ErrorMessage = "Username is required")] // Required attribute
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email format")] // EmailAddress attribute
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [MaxLength(100)]
    [DataType(DataType.Password)] // Password field
    public string PasswordHash { get; set; } = string.Empty;
    
    // Many-to-many relationship with Role table
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}