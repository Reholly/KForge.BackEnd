using Application.DTO.Auth;
using Application.Models;
using Application.Requests.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;

namespace Application.Handlers.Auth;

public class RegisterHandler(
    IRegistrationService registrationService, 
    IValidator<IdentityUserDto> identityValidator,
    IValidator<ApplicationUserDto> userValidator)
{
    private readonly IRegistrationService _registrationService = registrationService;
    private readonly IValidator<IdentityUserDto> _identityValidator = identityValidator;
    private readonly IValidator<ApplicationUserDto> _userValidator = userValidator;

    public async Task HandleAsync(RegisterRequest request, CancellationToken ct = default)
    {
        await _identityValidator.ValidateAndThrowAsync(request.IdentityUserDto, ct);
        await _userValidator.ValidateAndThrowAsync(request.ApplicationUserDto, ct);

        await _registrationService.RegisterAsync(request.ApplicationUserDto, request.IdentityUserDto);
    }
}