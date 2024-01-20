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
            throw new RoleException("User not found.");

        var result = await _userManager.AddToRoleAsync(user, role);
        
        if(!result.Succeeded)
            throw new RoleException(
                $"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task AttachRoleAsync(string role, IdentityUser user)
    {
        if (user is null)
            throw new RoleException("User not found.");
        
        var result = await _userManager.AddToRoleAsync(user, role);
        if(!result.Succeeded)
            throw new RoleException(
                $"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task DetachRoleAsync(string role, string username)
    {
        if (string.IsNullOrEmpty(role))
            throw new RoleException("Role is empty.");
        if (string.IsNullOrEmpty(username))
            throw new RoleException("Username is empty.");

        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new RoleException("User does not exist.");
        
        var result = await _userManager.RemoveFromRoleAsync(user, role);
        
        if (!result.Succeeded)
            throw new RoleException(
                $"Can not create role because of errors: {GetErrorDescription(result)}");
    }

    public async Task DetachRoleAsync(string role, IdentityUser user)
    {
        if (string.IsNullOrEmpty(role))
            throw new RoleException("Role is empty.");
        if (user is null)
            throw new RoleException("User is null.");

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        
        if(!result.Succeeded) 
            throw new RoleException(
            $"Can not detach from role because of: {GetErrorDescription(result)}");
    }

    public async Task CreateRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new RoleException("Role is empty.");

        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        if (!result.Succeeded)
            throw new RoleException(
                $"Can not create role because of errors: {GetErrorDescription(result)}");
    }
    
    public async Task DeleteRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new RoleException("Role is empty.");

        var toDelete = await _roleManager.FindByNameAsync(role);
        
        if(toDelete is null)
            throw new RoleException("Role does not exist.");

        var result = await _roleManager.DeleteAsync(toDelete);
        
        if(!result.Succeeded)
            throw new RoleException(
                $"Can not create role because of errors: {GetErrorDescription(result)}");
    }
    
    private string GetErrorDescription(IdentityResult result)
    {
        return String.Join(",", result.Errors.Select(x => x.Description));
    }
}