using Application.Models;

namespace Application.Responses.Auth;

public record RefreshTokenResponse
{ 
    public required AuthTokensModel AuthTokensModel { get; set; }
}