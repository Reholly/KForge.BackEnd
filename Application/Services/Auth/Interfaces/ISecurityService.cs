using Application.DTO.Security;

namespace Application.Services.Auth.Interfaces;

public interface ISecurityService
{
    Task ResetPasswordAsync(ResetPasswordDto dto);
}