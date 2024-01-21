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
    IMemoryCache refreshTokensCache)
{
    private readonly IJwtTokenService _tokenService = tokenService;
    private readonly IMemoryCache _refreshTokensCache = refreshTokensCache;

    public async Task<RefreshTokenResponse> HandleAsync(RefreshTokenDto dto, CancellationToken ct = default)
    {
        _refreshTokensCache.TryGetValue(dto.RefreshToken, out string? oldUsername);

        if (oldUsername is null)
            throw new UnauthorizedException("Refresh token is expired or invalid.");

        bool isValid = await _tokenService.IsJwtTokenValidAsync(dto.ExpiredAccessToken, false);
        
        if (!isValid)
            throw new UnauthorizedException("Expired access token is invalid by Key. ");
        
        var refreshTokenClaims = _tokenService.ParseClaims(dto.RefreshToken);
        
        var username = refreshTokenClaims.First(x => x.Type == ClaimTypes.UserData).Value.ToString();

        if (username != oldUsername)
            throw new UnauthorizedException("Refresh token is invalid. No matches access with refresh tokens.");
        
        var newRefreshToken = _tokenService.GenerateRefreshToken([new Claim(ClaimTypes.UserData, username)]);
        var newAccessToken = _tokenService.GenerateAccessToken(_tokenService.ParseClaims(dto.ExpiredAccessToken));

        _refreshTokensCache.Set(newRefreshToken, username);
        _refreshTokensCache.Remove(dto.RefreshToken);

        var authTokens = new AuthTokensModel(newRefreshToken, newAccessToken);
        
        return new RefreshTokenResponse
        {
            AuthTokensModel = authTokens
        };
    }
}