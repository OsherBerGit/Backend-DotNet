using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs.UserDtos;
using MyBackend.Exceptions;
using MyBackend.Mappers;
using MyBackend.Models;

namespace MyBackend.Services;

public class UserService(AppDbContext context, IUserMapper mapper) : IUserService
{
    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        var existingUser = await context.Users.AnyAsync(u => u.Username == dto.Username);
        if (existingUser)
            throw new UserAlreadyExistsException("Username is already taken.");
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var user = mapper.ToEntity(dto, hashedPassword);

        var defaultRole = await context.Roles.FirstOrDefaultAsync(r => r.Rolename == "User");
        if (defaultRole is not null)
            user.Roles = new List<Role> { defaultRole };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return mapper.ToDto(user)!;
    }
    
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        // load roles via eager loading
        // var users = await _context.Users
        //     .Include(u => u.UserRoles)
        //     .ThenInclude(ur => ur.Role)
        //     .ToListAsync();
        
        var users = await context.Users
            .AsNoTracking()
            .Include(u => u.Roles)  // load roles via skip navigation
            .ToListAsync();

        return users.Select(u => mapper.ToDto(u)!).ToList();
    }
    
    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);

        return mapper.ToDto(user);
    }
    
    public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if (user is null)
            return null;
        
        user.Email = dto.Email ?? user.Email;

        await context.SaveChangesAsync();

        return mapper.ToDto(user);
    }
    
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null)
            return false;

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return true;
    }
}
