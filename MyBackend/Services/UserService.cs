using Microsoft.AspNetCore.Identity;
using MyBackend.DTOs;
using MyBackend.Mappers;
using MyBackend.Models;
using MyBackend.Repositories;

namespace MyBackend.Services;

public class UserService
{
    private readonly UserRepository _repository;
    private readonly UserMapper _mapper;
    
    public UserService(UserRepository repository, UserMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _repository.GetAllAsync();
        return _mapper.ToDtoList(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user == null ? null : _mapper.ToDto(user);
    }

    public async Task<UserDto> CreateUserAsync(UserDto dto)
    {
        var user = _mapper.ToEntity(dto);
        var created = await _repository.AddAsync(user);
        return _mapper.ToDto(created);
    }

    public async Task UpdateUserAsync(int id, UserDto dto)
    {
        var user = _mapper.ToEntity(dto);
        user.Id = id;
        await _repository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user != null)
            await _repository.DeleteAsync(user);
    }

    public bool UserExists(int id) => _repository.Exists(id);
}