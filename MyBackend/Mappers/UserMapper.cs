using MyBackend.DTOs.UserDtos;
using MyBackend.Models;

namespace MyBackend.Mappers;

public class UserMapper
{
    public UserDto ToDto(User user)
    { 
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = user.UserRoles?
                .Select(ur => ur.Role.Rolename)
                .ToList() ?? new List<string>()
        };
    }
}