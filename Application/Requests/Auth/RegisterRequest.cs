using Application.DTO.Auth;
using Application.Models;

namespace Application.Requests.Auth;

public record RegisterRequest(
    ApplicationUserDto ApplicationUserDto, 
    IdentityUserDto IdentityUserDto);