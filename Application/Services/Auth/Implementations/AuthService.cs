using System.Collections.Specialized;
using System.Security.Claims;
using System.Web;
using Application.Exceptions.Auth;
using Application.Extensions;
using Application.Models;
using Application.Services.Auth.Interfaces;
using Application.Services.Utils.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Auth.Implementations;

public class AuthService : IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IEmailService _emailService;
    private readonly IJwtTokenService _tokenService;
    private readonly IConfiguration _configuration;
    
    private readonly IUserRepository _userRepository;
    
    public AuthService(
        SignInManager<IdentityUser> signInManager, 
        UserManager<IdentityUser> userManager, 
        IJwtTokenService tokenService,
        IConfiguration configuration, 
        IUserRepository userRepository, IEmailService emailService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _configuration = configuration;
        _userRepository = userRepository;
        _emailService = emailService;
    }
    
    public async Task<AuthTokensModel> LoginAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        
        if (!result.Succeeded)
            throw new LoginFailedException("Not allowed.");

        var user = await _userManager.FindByNameAsync(username);
        
        if (user is null)
            throw new LoginFailedException("User does not exist.");

        if (!user.EmailConfirmed)
            throw new LoginFailedException("Confirm your email.");
        
        var roles = await  _userManager.GetRolesAsync(user);

        var claims = new List<Claim>();

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        
        claims.Add(new Claim(ClaimTypes.UserData, username));

        var accessTokenExpiresInSeconds = int.Parse(_configuration["Jwt:AccessTokenExpiresInSeconds"]!);
        var refreshTokenExpiresInSeconds = int.Parse(_configuration["Jwt:RefreshTokenExpiresInSeconds"]!);
        
        var accessToken = _tokenService.GenerateAccessToken(accessTokenExpiresInSeconds, claims.ToArray());
        var refreshToken = _tokenService.GenerateRefreshToken(refreshTokenExpiresInSeconds, username);

        return new AuthTokensModel(
            refreshToken, 
            accessToken, 
            accessTokenExpiresInSeconds, 
            refreshTokenExpiresInSeconds); 
    }

    public async Task RegisterAsync(string username, string email, string password, ApplicationUser applicationUser, string role = "Student")
    {
        var userByEmail = await _userManager.FindByEmailAsync(email);
        var userByUsername = await _userManager.FindByNameAsync(username);
        
        if (userByEmail is not null)
            throw new RegistrationFailedException($"User with email: {email} already exists.");
        if (userByUsername is not null)
            throw new RegistrationFailedException($"User with username: {username} already exists.");

        var user = new IdentityUser
        { 
            UserName = username, 
            Email = email
        };
        
        var result = await _userManager.CreateAsync(user, password);
        
        if (!result.Succeeded)
            throw new RegistrationFailedException(String.Join(",", result.Errors.Select(x => x.Description)));

        var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var callbackConfirmationUrl = new Uri(_configuration["Auth:ConfirmationUrl"]!)
            .AddQuery("username", username)
            .AddQuery("code", confirmationCode);
        
        await _emailService.SendEmailAsync(
            user.Email,
            "Confirmation account",
            $"Confirm your email: <a href='{callbackConfirmationUrl}'>link</a>");

        var registeredUser = await _userManager.FindByEmailAsync(email);
                
        var roleAttachingResult = await _userManager.AddToRoleAsync(registeredUser!, role);

        await _userRepository.CreateAsync(applicationUser);
        await _userRepository.CommitAsync();
        
        if (!roleAttachingResult.Succeeded)
            throw new RoleAttachingException(String.Join(",", roleAttachingResult.Errors.Select(x => x.Description)));
    }

    public async Task ConfirmEmailAsync(string username, string code)
    {
        var user = await _userManager.FindByNameAsync(username);
        
        if (user is null)
            throw new RegistrationFailedException("No user with such username.");
        
        if(code is null)
            throw new RegistrationFailedException("Invalid confirmation code.");

        var result = await _userManager.ConfirmEmailAsync(user, code);
        
        if(!result.Succeeded)
            throw new RegistrationFailedException(String.Join(",", result.Errors.Select(x => x.Description)));
    }

    public async Task ResetPasswordAsync(string username, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            throw new PasswordResetException("Could not reset password: user with such username does no exist.");
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        await _userManager.ResetPasswordAsync(user, token, newPassword);
    }
}