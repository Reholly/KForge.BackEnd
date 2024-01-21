using Application.DTO.Auth;
using Application.Options;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Handlers.Auth;

public class LoginHandler(
    IAuthService authService,
    IValidator<LogInDto> validator,
    IOptions<JwtOptions> options,
    IMemoryCache refreshTokensCache)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<LogInDto> _validator = validator;
    private readonly JwtOptions _options = options.Value;
    private readonly IMemoryCache _refreshTokensCache = refreshTokensCache;

    public async Task<LoginResponse> HandleAsync(LogInDto request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        var loginModel = await _authService.LoginAsync(request.Username, request.Password);
        
        _refreshTokensCache.Set(loginModel.RefreshToken, request.Username, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(_options.RefreshTokenExpiresInSeconds)));
        
        return new LoginResponse
        {
            AuthTokensModel = loginModel
        };
    }
}