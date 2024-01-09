using Application.Models;

namespace Application.Responses.Auth;

public record LoginResponse
{
    public required AuthTokensModel AuthTokensModel { get; set; }
}