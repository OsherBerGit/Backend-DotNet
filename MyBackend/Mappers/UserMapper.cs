using MyBackend.DTOs;
using MyBackend.Models;
using MyBackend.Services;

namespace MyBackend.Mappers;

public class UserMapper
{
    private readonly RoleService _roleService;
    
    public UserMapper(RoleService roleService) { _roleService = roleService; }
    
    public UserDto ToDto(User user)
    {
        if (user == null) return null;
        
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Roles = user.UserRoles?
                .Select(ur => ur.Role.Name)
                .ToList() ?? new List<string>()
        };
    }

    public User ToEntity(UserDto dto)
    {
        if (dto == null) return null;
        
        return new User
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password,
            UserRoles = Nullable
        };
    }
}