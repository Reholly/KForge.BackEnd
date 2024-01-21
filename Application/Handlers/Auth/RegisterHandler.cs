using Application.DTO.Auth;
using Application.Models;
using Application.Requests.Auth;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Handlers.Auth;

public class RegisterHandler(
    IAuthService authService, 
    IValidator<IdentityUserDto> identityValidator,
    IValidator<ApplicationUserDto> userValidator)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<IdentityUserDto> _identityValidator = identityValidator;
    private readonly IValidator<ApplicationUserDto> _userValidator = userValidator;

    public async Task HandleAsync(RegisterRequest request, CancellationToken ct = default)
    {
        await _identityValidator.ValidateAndThrowAsync(request.IdentityUserDto, ct);
        await _userValidator.ValidateAndThrowAsync(request.ApplicationUserDto, ct);
        
        await _authService.RegisterAsync(
            request.IdentityUserDto.Username, 
            request.IdentityUserDto.Email, 
            request.IdentityUserDto.Password, 
            new ApplicationUser
            {
                BirthDate = request.ApplicationUserDto.BirthDate,
                Name = request.ApplicationUserDto.Name,
                Username = request.IdentityUserDto.Username,
                Surname = request.ApplicationUserDto.Surname,
                Patronymic = request.ApplicationUserDto.Patronymic,
            });
    }
}