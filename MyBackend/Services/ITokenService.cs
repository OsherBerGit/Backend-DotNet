using MyBackend.Models;

namespace MyBackend.Services;

public interface ITokenService
{
    string CreateToken(User user);
}