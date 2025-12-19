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

public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
    {
        try
        {
            var token = await authService.LoginUserAsync(authenticationRequest);
            return Ok("Logged in");
        }
        catch (Exception e)
        {
            return Unauthorized("Invalid username or password");
        }
    }
}