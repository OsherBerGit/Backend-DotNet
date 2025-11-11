using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

[Table("UserRoles")]
public class UserRole
{
    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public int RoleId { get; set; }
    
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}