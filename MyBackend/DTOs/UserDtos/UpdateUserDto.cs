using System.ComponentModel.DataAnnotations;

namespace MyBackend.DTOs.UserDtos;

public class UpdateUserDto
{
    [EmailAddress]
    public string? Email { get; set; }
    public List<string>? Roles { get; set; }
}