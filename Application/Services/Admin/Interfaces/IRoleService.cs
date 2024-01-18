using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.Interfaces;

public interface IRoleService
{
    Task AttachRoleAsync(string role, string username);
    Task AttachRoleAsync(string role, IdentityUser user);
    Task CreateRoleAsync(string role);
    Task DeleteRoleAsync(string role);
}