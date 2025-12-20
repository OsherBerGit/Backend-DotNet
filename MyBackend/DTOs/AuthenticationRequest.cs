using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs;

public class AuthenticationRequest // LoginUserDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}