using System.Security.Claims;

namespace Application.Services.Auth.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(int expiresInSeconds, Claim[] claims);
    string GenerateRefreshToken(int expiresInSeconds, string username);
    Task<bool> IsJwtTokenValidAsync(string jwtToken, bool validateLifeTime);
    Claim[] ParseClaims(string jwtToken);
}