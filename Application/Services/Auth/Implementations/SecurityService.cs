using Application.DTO.Security;
using Application.Exceptions.Common;
using Application.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Auth.Implementations;

public class SecurityService(
    UserManager<IdentityUser> userManager, 
    SignInManager<IdentityUser> signInManager)
    : ISecurityService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
     public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);

        if (user is null)
            throw new NotFoundException("Could not reset password: user with such username does no exist.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.OldPassword, false);
        
        if (!result.Succeeded)
            throw new ServiceException("Invalid password.");
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);
    }
}