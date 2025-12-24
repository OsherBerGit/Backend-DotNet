using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBackend.Models;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
    
    public DateTime? Revoked { get; set; }
    
    public bool IsActive => Revoked == null && DateTime.UtcNow < Expires;
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}