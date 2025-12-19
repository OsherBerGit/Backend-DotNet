using MyBackend.DTOs;
using MyBackend.DTOs.UserDtos;
using MyBackend.Models;

namespace MyBackend.Services;

public interface IAuthService
{
    Task<User?> RegisterUserAsync(AuthenticationRequest request);
    Task<string?> LoginUserAsync(AuthenticationRequest request);
}