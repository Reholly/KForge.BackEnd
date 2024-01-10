namespace Application.Models;

public record AuthTokensModel(
    string RefreshToken,
    string AccessToken,
    int AccessTokenExpiresInSeconds,
    int RefreshTokenExpiresInSeconds);
