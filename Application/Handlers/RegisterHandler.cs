using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;

namespace Application.Handlers;

public class RegisterHandler(IAuthService authService, IValidator<RegisterRequest> validator)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<RegisterRequest> _validator = validator;

    public async Task<RegisterResponse> HandleAsync(RegisterRequest request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        await _authService.RegisterAsync(request.Email, request.Password);

        return new RegisterResponse();
    }
}