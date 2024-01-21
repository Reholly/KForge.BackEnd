namespace Application.Options;

public class JwtOptions 
{
    public const string Section = "Auth";
    
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public int RefreshTokenExpiresInSeconds { get; set; } 
    public int AccessTokenExpiresInSeconds { get; set; }
}