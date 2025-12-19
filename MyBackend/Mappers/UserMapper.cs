using MyBackend.DTOs.UserDtos;
using MyBackend.Models;

namespace MyBackend.Mappers;

public class UserMapper : IUserMapper
{
    public UserDto ToDto(User user)
    { 
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = user.Roles?
                .Select(ur => ur.Rolename)
                .ToList() ?? new List<string>()
        };
    }

    public User ToEntity(CreateUserDto dto, string hashedPassword)
    {
        return new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = hashedPassword
        };
    }
}