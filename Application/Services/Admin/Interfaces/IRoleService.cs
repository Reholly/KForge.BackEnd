using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.Interfaces;

public interface IRoleService
{
    Task AttachDefaultRolesAsync(string username);
    Task AttachRoleAsync(string role, string username);
    Task AttachRoleAsync(string role, IdentityUser user);

    Task DetachRoleAsync(string role, string username);
    Task DetachRoleAsync(string role, IdentityUser user);
    Task CreateRoleAsync(string role);
    Task DeleteRoleAsync(string role);
}