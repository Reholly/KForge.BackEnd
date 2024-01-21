using Microsoft.Extensions.Options;

namespace Application.Configuration.Options;

public class JwtOptions : IOptions<JwtOptions>
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Key { get; init; } = string.Empty;
    public int RefreshTokenExpiresInSeconds { get; init; } 
    public int AccessTokenExpiresInSeconds { get; init; }

    public JwtOptions Value => this;
}