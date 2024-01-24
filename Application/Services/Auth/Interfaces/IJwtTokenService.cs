using System.Security.Claims;

namespace Application.Services.Auth.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(Claim[] claims);
    string GenerateRefreshToken(Claim[] claims);
    Claim[] ParseClaims(string jwtToken);
    Task<bool> IsJwtTokenValidAsync(string jwtToken, bool validateLifeTime);
    string GetUsernameFromAccessToken(string jwtToken);
}