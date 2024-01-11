using Application.Models;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Handlers.Auth;

public class RegisterHandler(
    IAuthService authService, 
    IValidator<IdentityModel> identityValidator,
    IValidator<UserModel> userValidator)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<IdentityModel> _identityValidator = identityValidator;
    private readonly IValidator<UserModel> _userValidator = userValidator;

    public async Task<RegisterResponse> HandleAsync(RegisterRequest request, CancellationToken ct = default)
    {
        await _identityValidator.ValidateAndThrowAsync(request.IdentityModel, ct);
        await _userValidator.ValidateAndThrowAsync(request.UserModel, ct);
        
        await _authService.RegisterAsync(
            request.IdentityModel.Username, 
            request.IdentityModel.Email, 
            request.IdentityModel.Password, 
            new ApplicationUser
            {
                BirthDate = request.UserModel.BirthDate,
                Name = request.UserModel.Name,
                Username = request.IdentityModel.Username,
                Surname = request.UserModel.Surname,
                Patronymic = request.UserModel.Patronymic,
            });

        return new RegisterResponse();
    }
}