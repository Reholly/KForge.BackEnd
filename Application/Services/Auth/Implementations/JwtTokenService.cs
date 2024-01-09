using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Auth.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateAccessToken(int expiresInSeconds, Claim[] claims)
    {
        return GenerateJwtToken(expiresInSeconds, claims);
    }

    public string GenerateRefreshToken(int expiresInSeconds, string email)
    {
        return GenerateJwtToken(expiresInSeconds, [new Claim(ClaimTypes.Email, email)]);
    }

    public async Task<bool> IsJwtTokenValidAsync(string jwtToken, bool validateLifeTime)
    {
        var validationParameters = new TokenValidationParameters {
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
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
        catch (SecurityTokenValidationException e)
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
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddSeconds(expiresInSeconds),
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}