using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Handlers;

public class LoginHandler(
    IAuthService authService,
    IValidator<LoginRequest> validator,
    IMemoryCache refreshTokensCache)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<LoginRequest> _validator = validator;
    private readonly IMemoryCache _refreshTokensCache = refreshTokensCache;

    public async Task<LoginResponse> HandleAsync(LoginRequest request, CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        var loginModel = await _authService.LoginAsync(request.Email, request.Password);
        
        _refreshTokensCache.Set(loginModel.RefreshToken, request.Email, new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(loginModel.RefreshTokenExpiresInSeconds)));
        
        return new LoginResponse
        {
            AuthTokensModel = loginModel
        };
    }
}