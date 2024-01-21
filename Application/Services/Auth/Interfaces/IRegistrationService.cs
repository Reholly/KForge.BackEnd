using Application.DTO.Auth;
using Application.Models;

namespace Application.Services.Auth.Interfaces;

public interface IRegistrationService
{
    Task RegisterAsync(ApplicationUserDto applicationUser, IdentityUserDto identityUserDto);
    Task ConfirmEmailAsync(ConfirmEmailDto dto);
}