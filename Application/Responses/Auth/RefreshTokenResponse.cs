using Application.Models;

namespace Application.Responses.Auth;

public record RefreshTokenResponse(AuthTokensModel AuthTokensModel);