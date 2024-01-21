using Application.Models;

namespace Application.Requests.Auth;

public record RegisterRequest(
    ApplicationUserDto ApplicationUserDto, 
    IdentityModel IdentityModel);