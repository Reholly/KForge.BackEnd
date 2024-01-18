using Application.Exceptions.Auth;
using Application.Services.Admin.Interfaces;
using Application.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.Implementations;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    
    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
    public async Task AttachRoleAsync(string role, string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new RoleAttachingException("User not found.");

        var result = await _userManager.AddToRoleAsync(user, role);
        
        if(!result.Succeeded)
            throw new RoleAttachingException(
                $"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task AttachRoleAsync(string role, IdentityUser user)
    {
        if (user is null)
            throw new RoleAttachingException("User not found.");
        
        var result = await _userManager.AddToRoleAsync(user, role);
        if(!result.Succeeded)
            throw new RoleAttachingException(
                $"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task CreateRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new RoleAttachingException("Role is empty.");

        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        if (!result.Succeeded)
            throw new RoleAttachingException(
                $"Can not create role because of errors: {GetErrorDescription(result)}");
    }
    
    public async Task DeleteRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new RoleAttachingException("Role is empty.");

        var toDelete = await _roleManager.FindByNameAsync(role);
        
        if(toDelete is null)
            throw new RoleAttachingException("Role does not exist.");

        var result = await _roleManager.DeleteAsync(toDelete);
        
        if(!result.Succeeded)
            throw new RoleAttachingException(
                $"Can not create role because of errors: {GetErrorDescription(result)}");
    }
    
    private string GetErrorDescription(IdentityResult result)
    {
        return String.Join(",", result.Errors.Select(x => x.Description));
    }
}