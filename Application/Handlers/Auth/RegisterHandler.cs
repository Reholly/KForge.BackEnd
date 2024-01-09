using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Handlers.Auth;

public class RegisterHandler(IAuthService authService, IValidator<RegisterRequest> validator)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<RegisterRequest> _validator = validator;

    public async Task<RegisterResponse> HandleAsync(RegisterRequest request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        await _authService.RegisterAsync(request.Username, request.Email, request.Password, 
            new ApplicationUser
            {
                BirthDate = request.ApplicationUserModel.BirthDate,
                Name = request.ApplicationUserModel.Name,
                Username = request.Username,
                Surname = request.ApplicationUserModel.Surname,
                Patronymic = request.ApplicationUserModel.Patronymic,
            });

        return new RegisterResponse();
    }
}