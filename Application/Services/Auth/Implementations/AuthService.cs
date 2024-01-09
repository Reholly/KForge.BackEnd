using System.Security.Claims;
using Application.Exceptions.Auth;
using Application.Models;
using Application.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Auth.Implementations;

public class AuthService : IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    
    private readonly IJwtTokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    public AuthService(
        SignInManager<IdentityUser> signInManager, 
        UserManager<IdentityUser> userManager, 
        IJwtTokenService tokenService,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    
    public async Task<AuthTokensModel> LoginAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        
        if (!result.Succeeded)
            throw new LoginFailedException("Not allowed.", 400);

        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null)
            throw new LoginFailedException("User does not exist.", 400);
        
        
        
        var roles = await  _userManager.GetRolesAsync(user);

        var claims = new List<Claim>();

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        
        claims.Add(new Claim(ClaimTypes.Email, email));

        var accessTokenExpiresInSeconds = int.Parse(_configuration["Jwt:AccessTokenExpiresInSeconds"]!);
        var refreshTokenExpiresInSecond = int.Parse(_configuration["Jwt:RefreshTokenExpiresInSeconds"]!);
        
        var accessToken = _tokenService.GenerateAccessToken(accessTokenExpiresInSeconds, claims.ToArray());
        var refreshToken = _tokenService.GenerateRefreshToken(refreshTokenExpiresInSecond, email);

      

        return new AuthTokensModel
        {
            AccessToken = accessToken, 
            AccessTokenExpiresInSeconds = accessTokenExpiresInSeconds, 
            RefreshTokenExpiresInSeconds = refreshTokenExpiresInSecond,
            RefreshToken = refreshToken
        };
    }

    public async Task RegisterAsync(string email, string password, string role = "Student")
    {
        var userByEmail = await _userManager.FindByEmailAsync(email);
        
        if (userByEmail is not null)
            throw new RegistrationFailedException($"User with {email} already exists.", 400);

        var user = new IdentityUser
        { 
            UserName = email, 
            Email = email 
        };
        
        var result = await _userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
            throw new RegistrationFailedException(String.Join(",", result.Errors.Select(x => x.Description)), 400);
        
        var registeredUser = await _userManager.FindByEmailAsync(email);
                
        var roleAttachingResult = await _userManager.AddToRoleAsync(registeredUser!, role);
        
        if (!roleAttachingResult.Succeeded)
            throw new RoleAttachingException(String.Join(",", roleAttachingResult.Errors.Select(x => x.Description)), 400);
    }
}