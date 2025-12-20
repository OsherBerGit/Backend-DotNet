using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
using MyBackend.Models;

namespace MyBackend.Services;

public class AuthService(AppDbContext context, ITokenService tokenService) : IAuthService
{
    public async Task<User?> RegisterUserAsync(CreateUserDto request)
    {
        if (await context.Users.AnyAsync(u => u.Username == request.Username))
            return null;
        
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<string?> LoginUserAsync(AuthenticationRequest request)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user is null) return null;
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;
        
        return tokenService.CreateToken(user);
    }
}