using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;

namespace MyBackend.Repositories;

public class RoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context) { _context = context; }

    public async Task<List<Role>> FindRolesByUserIdAsync(long userId)
    {
        return await _context.Roles
            .Where(r => r.UserRoles.Any(ur => ur.UserId == userId))
            .ToListAsync();
    }

    public async Task<Role?> FindByRoleNameAsync(string roleName)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
    }

    public async Task<List<Role>> GetAllAsync() { return await _context.Roles.ToListAsync(); }

    public async Task AddAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
    }
}