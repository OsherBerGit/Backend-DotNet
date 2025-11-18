using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs;
using MyBackend.Models;

namespace MyBackend.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    
    public AuthService(AppDbContext context) { _context = context; }

    public async Task<User?> ValidateUserAsync(AuthenticationRequest authenticationRequest)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == authenticationRequest.Username);

        if (user == null) return null;

        var validPassword = BCrypt.Net.BCrypt.Verify(authenticationRequest.Password, user.Password);

        return !validPassword ? null : user;
    }
}