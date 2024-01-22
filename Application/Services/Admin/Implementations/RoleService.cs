using Application.Exceptions.Common;
using Application.Services.Admin.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Admin.Implementations;

public class RoleService(
    RoleManager<IdentityRole> roleManager, 
    UserManager<IdentityUser> userManager)
    : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    private const string DefaultRole = "Student";

    public async Task AttachDefaultRolesAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        
        if (user is null)
            throw new NotFoundException("User not found.");

        var result = await _userManager.AddToRoleAsync(user, DefaultRole);
        
        if(!result.Succeeded)
            throw new ServiceException($"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task AttachRoleAsync(string role, string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new NotFoundException("User not found.");

        var result = await _userManager.AddToRoleAsync(user, role);
        
        if(!result.Succeeded)
            throw new ServiceException($"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task AttachRoleAsync(string role, IdentityUser user)
    {
        if (user is null)
            throw new NotFoundException("User not found.");
        
        var result = await _userManager.AddToRoleAsync(user, role);
        if(!result.Succeeded)
            throw new ServiceException($"Can not attach to user to role because of: {GetErrorDescription(result)}");
    }

    public async Task DetachRoleAsync(string role, string username)
    {
        if (string.IsNullOrEmpty(role))
            throw new NotFoundException("Role is empty.");
        if (string.IsNullOrEmpty(username))
            throw new NotFoundException("Username is empty.");

        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new NotFoundException("User does not exist.");
        
        var result = await _userManager.RemoveFromRoleAsync(user, role);
        
        if (!result.Succeeded)
            throw new ServiceException($"Can not create role because of errors: {GetErrorDescription(result)}");
    }

    public async Task DetachRoleAsync(string role, IdentityUser user)
    {
        if (string.IsNullOrEmpty(role))
            throw new NotFoundException("Role is empty.");
        if (user is null)
            throw new NotFoundException("User is null.");

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        
        if(!result.Succeeded) 
            throw new ServiceException($"Can not detach from role because of: {GetErrorDescription(result)}");
    }

    public async Task CreateRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new NotFoundException("Role is empty.");

        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        if (!result.Succeeded)
            throw new ServiceException($"Can not create role because of errors: {GetErrorDescription(result)}");
    }
    
    public async Task DeleteRoleAsync(string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new NotFoundException("Role is empty.");

        var toDelete = await _roleManager.FindByNameAsync(role);
        
        if(toDelete is null)
            throw new NotFoundException("Role does not exist.");

        var result = await _roleManager.DeleteAsync(toDelete);
        
        if(!result.Succeeded)
            throw new ServiceException($"Can not create role because of errors: {GetErrorDescription(result)}");
    }
    
    private string GetErrorDescription(IdentityResult result)
    {
        return String.Join(",", result.Errors.Select(x => x.Description));
    }
}