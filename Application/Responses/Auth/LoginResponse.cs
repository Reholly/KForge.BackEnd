using Application.Models;

namespace Application.Responses.Auth;

public record LoginResponse(AuthTokensModel AuthTokensModel);