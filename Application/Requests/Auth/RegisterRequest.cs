using Application.Models;

namespace Application.Requests.Auth;

public record RegisterRequest(
    UserModel UserModel, 
    IdentityModel IdentityModel);