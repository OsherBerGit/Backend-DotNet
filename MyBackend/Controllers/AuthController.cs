using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
using MyBackend.Models;
using MyBackend.Services;

namespace MyBackend.Controllers;

[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto request)
    {
        var user = await authService.RegisterUserAsync(request);
        
        if (user is null)
            return BadRequest("Username is already taken");
        
        return Ok(new { message = "User registered successfully", userId = user.Id });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
    {
        var token = await authService.LoginUserAsync(authenticationRequest);
        
        if (token is null)
            return Unauthorized("Invalid username or password");
        
        return Ok(new {token});
    }
}