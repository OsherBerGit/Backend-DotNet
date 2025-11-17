using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
using MyBackend.Models;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("/users")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }
    
    [HttpGet("/users/{id}")]
    public async Task<ActionResult<UserDto?>> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }
    
    [HttpPost("/users")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        return await _userService.CreateUser(dto);
    }
    
    [HttpPut("/users/{id}")]
    public async Task<ActionResult<UserDto?>> UpdateUser(int id, UpdateUserDto dto)
    {
        return await _userService.UpdateUser(id, dto);
    }
    
    [HttpDelete("/users/{id}")]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }
}