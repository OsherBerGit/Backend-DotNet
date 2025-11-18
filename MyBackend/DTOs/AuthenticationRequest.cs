namespace MyBackend.DTOs;

public class AuthenticationRequest // LoginUserDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}