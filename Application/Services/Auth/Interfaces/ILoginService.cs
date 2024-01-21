using Application.DTO.Auth;
using Application.Models;

namespace Application.Services.Auth.Interfaces;

public interface ILoginService
{
    Task<AuthTokensModel> LogInAsync(LogInDto dto);
}