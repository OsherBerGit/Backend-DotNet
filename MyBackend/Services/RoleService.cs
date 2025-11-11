using MyBackend.Models;
using MyBackend.Repositories;

namespace MyBackend.Services;

public class RoleService
{
    private readonly RoleRepository _roleRepository;

    public RoleService(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role?> GetRoleByIdAsync(long id)
    {
        return await _roleRepository.GetAllAsync()
            .ContinueWith(t => t.Result.Find(r => r.Id == id));
    }

    public async Task<Role?> FindByRoleNameAsync(string roleName)
    {
        return await _roleRepository.FindByRoleNameAsync(roleName);
    }
}