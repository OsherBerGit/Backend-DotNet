using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyBackend.DTOs;
using MyBackend.Services;

namespace MyBackend.Controllers;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService) { _authService = authService; }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
    {
        var user = await _authService.ValidateUserAsync(authenticationRequest);
        if (user == null)
            return Unauthorized("Invalid username or password");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        if (user.Roles != null)
            claims.AddRange(user.Roles.Select(ur => new Claim(ClaimTypes.Role, ur.Rolename)));

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            authProperties
        );

        return Ok("Logged in");
    }
}