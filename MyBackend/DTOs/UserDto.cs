namespace MyBackend.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    public List<string> Roles { get; set; } = new();
}