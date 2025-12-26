using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs.UserDtos;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
[Route("api/users")]
[Authorize] // any authenticated user
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users); // 200
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
            return NotFound(); // 404
        return Ok(user); // 200
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        try
        {
            var newUser = await userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser); // 201
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto?>> UpdateUser(int id, UpdateUserDto dto)
    {
        try
        {
            var updatedUser = await userService.UpdateUserAsync(id, dto);
            if (updatedUser is null)
                return NotFound(); // 404
            return Ok(updatedUser); // 200
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var success = await userService.DeleteUserAsync(id); // 204 or 404
            return success ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}