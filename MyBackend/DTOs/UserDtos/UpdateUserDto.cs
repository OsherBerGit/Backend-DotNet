using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs.UserDtos;

public class UpdateUserDto
{
    [EmailAddress]
    public string? Email { get; set; } = null!;
    public List<string>? Roles { get; set; }
}