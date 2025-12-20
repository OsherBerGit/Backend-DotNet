using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs.UserDtos;

public class CreateUserDto // RegisterUserDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}