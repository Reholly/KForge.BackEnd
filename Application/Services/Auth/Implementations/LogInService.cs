using System.Security.Claims;
using Application.DTO.Auth;
using Application.Exceptions.Common;
using Application.Models;
using Application.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Auth.Implementations;

public class LogInService(
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager,
    IJwtTokenService tokenService) 
    : ILogInService
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IJwtTokenService _tokenService = tokenService;

    public async Task<AuthTokensModel> LogInAsync(LogInDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
        
        if (!result.Succeeded)
            throw new UnauthorizedException("Not allowed.");

        var user = await _userManager.FindByNameAsync(dto.Username);
        
        var roles = await  _userManager.GetRolesAsync(user!);

        var claims = new List<Claim>();

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var usernameClaim = new Claim(ClaimTypes.UserData, dto.Username);
        claims.Add(usernameClaim);
        
        var accessToken = _tokenService.GenerateAccessToken(claims.ToArray());
        var refreshToken = _tokenService.GenerateRefreshToken([usernameClaim]);

        return new AuthTokensModel(refreshToken, accessToken);
    }
}