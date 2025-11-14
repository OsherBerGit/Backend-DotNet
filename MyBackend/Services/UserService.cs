using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;
using BCrypt.Net;

namespace MyBackend.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context) { _context = context; }
    
    public async Task<User> CreateUserAsync(string username, string email, string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); // unsynchronous save
        return user;
    }
    
    public async Task AssignRoleAsync(int userId, int roleId)
    {
        var exists = await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (!exists)
        {
            _context.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
    }
    
    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
