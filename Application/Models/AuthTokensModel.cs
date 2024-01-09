namespace Application.Models;

public class AuthTokensModel
{
    public required string RefreshToken { get; init; }
    public required string AccessToken { get; init; }
    public required int AccessTokenExpiresInSeconds { get; init; }
    public required int RefreshTokenExpiresInSeconds { get; init; }
}