using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.DTOs;
using MyBackend.Mappers;
using MyBackend.Models;
using MyBackend.Repositories;

namespace MyBackend.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly UserMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;

        _mapper.Map(dto, user);

        await _context.SaveChangesAsync();
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}