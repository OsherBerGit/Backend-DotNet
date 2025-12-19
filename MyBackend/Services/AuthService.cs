using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
using MyBackend.Models;

namespace MyBackend.Services;

public class AuthService(AppDbContext context) : IAuthService
{
    public async Task<User?> ValidateUserAsync(AuthenticationRequest authenticationRequest)
    {
        var user = await context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == authenticationRequest.Username);

        if (user == null) return null;

        var validPassword = BCrypt.Net.BCrypt.Verify(authenticationRequest.Password, user.PasswordHash);

        return !validPassword ? null : user;
    }

    public Task<User?> RegisterUserAsync(AuthenticationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> LoginUserAsync(AuthenticationRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user == null) return null;
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;
        
        throw new NotImplementedException();
    }
}