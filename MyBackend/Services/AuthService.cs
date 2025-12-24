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

    public async Task<AuthenticationResponse?> LoginUserAsync(AuthenticationRequest request)
    {
        var user = await context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user is null) return null;
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;
        
        var accessToken = tokenService.CreateToken(user);
        var refreshToken = tokenService.GenerateRefreshToken(user);
        
        user.RefreshTokens.Add(refreshToken); 
        await context.SaveChangesAsync();
        
        return new AuthenticationResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthenticationResponse?> RefreshTokenAsync(string token)
    {
        var user = await context.Users
            .Include(u => u.RefreshTokens) // Load the refresh tokens
            .Include(u => u.Roles)         // Load the roles for the new AccessToken
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        if (user is null) return null;
        
        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
        
        if (!refreshToken.IsActive) return null;
        
        refreshToken.Revoked = DateTime.UtcNow;
        
        var newAccessToken = tokenService.CreateToken(user);
        var newRefreshToken = tokenService.GenerateRefreshToken(user);

        user.RefreshTokens.Add(newRefreshToken);
    
        await context.SaveChangesAsync();

        return new AuthenticationResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var user = await context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        
        if (user is null) return false;
        
        var refreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == token);
        
        if (refreshToken is null || !refreshToken.IsActive) return false;
        
        refreshToken.Revoked = DateTime.UtcNow;
        
        await context.SaveChangesAsync();
        
        return true;
    }
}