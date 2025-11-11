using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.Models;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() =>
        Ok(await _service.GetAllUsersAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto dto)
    {
        var created = await _service.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserDto dto)
    {
        if (!_service.UserExists(id)) return NotFound();
        await _service.UpdateUserAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (!_service.UserExists(id)) return NotFound();
        await _service.DeleteUserAsync(id);
        return NoContent();
    }
}