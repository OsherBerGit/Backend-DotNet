using MyBackend.DTOs;
using MyBackend.Models;

namespace MyBackend.Mappers;

public class UserMapper
{
    public UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name
        };
    }

    public User ToEntity(UserDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }

    public List<UserDto> ToDtoList(List<User> users)
    {
        return users.Select(u => ToDto(u)).ToList();
    }
}