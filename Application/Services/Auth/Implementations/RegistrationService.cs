using Application.DTO.Auth;
using Application.Exceptions.Common;
using Application.Extensions;
using Application.Models;
using Application.Options;
using Application.Services.Admin.Interfaces;
using Application.Services.Auth.Interfaces;
using Application.Services.Utils.Interfaces;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Services.Auth.Implementations;

public class RegistrationService(
    UserManager<IdentityUser> userManager, 
    ApplicationDbContext dbContext,
    IOptions<AuthOptions> options,
    IRoleService roleService,
    IEmailService emailService) 
    : IRegistrationService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IRoleService _roleService = roleService;
    private readonly AuthOptions _options = options.Value;
    private readonly IEmailService _emailService = emailService;

    public async Task RegisterAsync(ApplicationUserDto applicationUser, IdentityUserDto identityUserDto)
    {
        var userByEmail = await _userManager.FindByEmailAsync(identityUserDto.Email);
        var userByUsername = await _userManager.FindByNameAsync(identityUserDto.Username);
        
        if (userByEmail is not null)
            throw new ConflictException($"User with email: {identityUserDto.Email} already exists.");
        if (userByUsername is not null)
            throw new ConflictException($"User with username: {identityUserDto.Username} already exists.");

        var user = new IdentityUser
        { 
            UserName = identityUserDto.Username, 
            Email = identityUserDto.Email
        };
        
        var result = await _userManager.CreateAsync(user, identityUserDto.Password);
        
        if (!result.Succeeded)
            throw new ServiceException(String.Join(",", result.Errors.Select(x => x.Description)));

        await SendEmailConfirmationMailAsync(identityUserDto, user);
        
        await _roleService.AttachDefaultRolesAsync(identityUserDto.Username);
        
        await _dbContext.Profiles.AddAsync(new ApplicationUser
        {
            Name = applicationUser.Name,
            Surname = applicationUser.Surname,
            Patronymic = applicationUser.Patronymic,
            BirthDate = applicationUser.BirthDate,
            Username = identityUserDto.Username
        });
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task ConfirmEmailAsync(ConfirmEmailDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        
        if (user is null)
            throw new NotFoundException("No user with such username.");

        var result = await _userManager.ConfirmEmailAsync(user, dto.Code);
        
        if(!result.Succeeded)
            throw new ServiceException(String.Join(",", result.Errors.Select(x => x.Description)));
    }
    
    private async Task SendEmailConfirmationMailAsync(IdentityUserDto identityUserDto, IdentityUser user)
    {
        var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var callbackConfirmationUrl = new Uri(_options.EmailConfirmationUrl)
            .AddQuery("username", identityUserDto.Username)
            .AddQuery("code", confirmationCode);
        
        await _emailService.SendEmailAsync(
            user.Email!,
            "Подтверждение регистрации",
            $"Для подтверждения регистрации перейдите по этой <a href='{callbackConfirmationUrl}'>ссылке.</a>." +
            $" Если это были не вы, просим вас связаться с тех.поддержкой сайта.");
    }
}