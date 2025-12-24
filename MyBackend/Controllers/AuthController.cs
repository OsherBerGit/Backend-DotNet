using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
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
    public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest authenticationRequest)
    {
        var result = await authService.LoginUserAsync(authenticationRequest);
        
        if (result is null)
            return Unauthorized("Invalid username or password");
        
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticationResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await authService.RefreshTokenAsync(request.Token);
        
        if (result is null)
            return Unauthorized("Invalid refresh token");
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RefreshTokenRequest request)
    {
        var success = await authService.RevokeTokenAsync(request.Token);
    
        if (!success)
            return BadRequest("Token is invalid or already revoked");

        return NoContent();
    }
}