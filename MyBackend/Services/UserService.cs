using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
using MyBackend.Mappers;

namespace MyBackend.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly UserMapper _mapper;

    public UserService(AppDbContext context, UserMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<UserDto> CreateUser(CreateUserDto dto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); // unsynchronous save

        return _mapper.ToDto(user);
    }
    
    public async Task<List<UserDto>> GetAllUsers()
    {
        // var users = await _context.Users load roles via eager loading
        //     .Include(u => u.UserRoles)
        //     .ThenInclude(ur => ur.Role)
        //     .ToListAsync();
        
        var users = await _context.Users
            .Include(u => u.Roles)  // load roles via skip navigation
            .ToListAsync();

        return users.Select(_mapper.ToDto).ToList();
    }
    
    public async Task<UserDto?> GetUserById(int id)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user is null ? null : _mapper.ToDto(user);
    }
    
    public async Task<UserDto?> UpdateUser(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return null;
        
        user.Email = dto.Email ?? user.Email;

        await _context.SaveChangesAsync();

        return _mapper.ToDto(user);
    }
    
    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
