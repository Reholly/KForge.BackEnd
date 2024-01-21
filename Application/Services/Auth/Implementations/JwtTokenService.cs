using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Options;
using Application.Services.Auth.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Auth.Implementations;

public class JwtTokenService(IOptions<JwtOptions> options) 
    : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;
    
    public string GenerateAccessToken(Claim[] claims)
    {
        return GenerateJwtToken(_options.AccessTokenExpiresInSeconds, claims);
    }

    public string GenerateRefreshToken(Claim[] claims)
    {
        return GenerateJwtToken(_options.RefreshTokenExpiresInSeconds, claims);
    }

    public async Task<bool> IsJwtTokenValidAsync(string jwtToken, bool validateLifeTime)
    {
        var validationParameters = new TokenValidationParameters {
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = validateLifeTime,
            ValidateIssuerSigningKey = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            await tokenHandler.ValidateTokenAsync(jwtToken, validationParameters);
            
            return true;
        }
        catch (SecurityTokenValidationException)
        {
            return false;
        }
    }

 
    public Claim[] ParseClaims(string jwtToken)
    {
        return new JwtSecurityTokenHandler()
            .ReadJwtToken(jwtToken)
            .Claims
            .ToArray();
    }

    private string GenerateJwtToken(int expiresInSeconds, Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddSeconds(expiresInSeconds),
            issuer: _options.Issuer,
            audience: _options.Audience,
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}