using System.Security.Claims;
using Application.Exceptions.Common;
using Application.Models;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Application.Handlers.Auth;

public class RefreshTokenHandler(
    IJwtTokenService tokenService, 
    IConfiguration configuration, 
    IMemoryCache refreshTokensCache)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IJwtTokenService _tokenService = tokenService;
    private readonly IMemoryCache _refreshTokensCache = refreshTokensCache;

    public async Task<RefreshTokenResponse> HandleAsync(RefreshTokenRequest request, CancellationToken ct = default)
    {
        var accessTokenExpiresInSeconds = int.Parse(_configuration["Jwt:AccessTokenExpiresInSeconds"]!);
        var refreshTokenExpiresInSecond = int.Parse(_configuration["Jwt:RefreshTokenExpiresInSeconds"]!);
        
        _refreshTokensCache.TryGetValue(request.RefreshToken, out string? email);

        if (email is null)
            throw new UnauthorizedException("Refresh token is expired or invalid.");

        bool isValid = await _tokenService.IsJwtTokenValidAsync(request.ExpiredAccessToken, false);
        if (!isValid)
            throw new UnauthorizedException("Expired access token is invalid by Key. ");
        
        var refreshTokenClaims = _tokenService.ParseClaims(request.RefreshToken);
        
        var refreshTokenEmail = refreshTokenClaims.First(x => x.Type == ClaimTypes.Email).Value.ToString();

        if (refreshTokenEmail != email)
            throw new UnauthorizedException("Refresh token is invalid. No matches access with refresh tokens.");

        var newRefreshToken = _tokenService.GenerateRefreshToken(refreshTokenExpiresInSecond, refreshTokenEmail);
        var newAccessToken = _tokenService.GenerateAccessToken(accessTokenExpiresInSeconds, _tokenService.ParseClaims(request.ExpiredAccessToken));

        _refreshTokensCache.Set(newRefreshToken, refreshTokenEmail);
        _refreshTokensCache.Remove(request.RefreshToken);
        
        var authTokens = new AuthTokensModel(
            newRefreshToken,
            newAccessToken,
            accessTokenExpiresInSeconds,
            refreshTokenExpiresInSecond);
        
        return new RefreshTokenResponse
        {
            AuthTokensModel = authTokens
        };
    }
}