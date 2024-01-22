using Application.DTO.Auth;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;

namespace Application.Handlers.Auth;

public class LogInHandler(
    IJwtTokenStorage storage,
    ILogInService logInService,
    IValidator<LogInDto> validator)
{
    private readonly ILogInService _logInService = logInService;
    private readonly IValidator<LogInDto> _validator = validator;
    private readonly IJwtTokenStorage _tokenStorage = storage;

    public async Task<LoginResponse> HandleAsync(LogInRequest request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request.LogInDto, ct);
        
        var tokens = await _logInService.LogInAsync(request.LogInDto);
        
        _tokenStorage.Set(request.LogInDto.Username, tokens.RefreshToken);

        return new LoginResponse(tokens);
    }
}