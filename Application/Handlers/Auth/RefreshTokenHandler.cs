using System.Security.Claims;
using Application.Exceptions.Common;
using Application.Models;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Application.Services.Auth.Interfaces;
namespace Application.Handlers.Auth;

public class RefreshTokenHandler(
    IJwtTokenService tokenService, 
    IJwtTokenStorage storage)
{
    private readonly IJwtTokenService _tokenService = tokenService;
    private readonly IJwtTokenStorage _storage = storage;

    public async Task<RefreshTokenResponse> HandleAsync(RefreshTokenRequest request, CancellationToken ct = default)
    {
        _storage.TryGetValue(request.RefreshTokenDto.RefreshToken, out string? oldUsername);

        if (oldUsername is null)
            throw new UnauthorizedException("Refresh token is expired or invalid.");

        bool isValid = await _tokenService.IsJwtTokenValidAsync(request.RefreshTokenDto.ExpiredAccessToken, false);
        
        if (!isValid)
            throw new UnauthorizedException("Expired access token is invalid.");
        
        var refreshTokenClaims = _tokenService.ParseClaims(request.RefreshTokenDto.RefreshToken);
        
        var username = refreshTokenClaims.First(x => x.Type == ClaimTypes.UserData).Value.ToString();

        if (username != oldUsername)
            throw new UnauthorizedException("Refresh token is invalid. No matches access with refresh tokens.");
        
        var newRefreshToken = _tokenService.GenerateRefreshToken(
            refreshTokenClaims);
        var newAccessToken = _tokenService.GenerateAccessToken(
            _tokenService.ParseClaims(request.RefreshTokenDto.ExpiredAccessToken));

        _storage.Set(username, newRefreshToken);
        _storage.Remove(request.RefreshTokenDto.RefreshToken);

        var authTokens = new AuthTokensModel(newRefreshToken, newAccessToken);

        return new RefreshTokenResponse(authTokens);
    }
}