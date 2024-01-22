using Application.DTO.Auth;
using Application.Models;

namespace Application.Services.Auth.Interfaces;

public interface ILogInService
{
    Task<AuthTokensModel> LogInAsync(LogInDto dto);
}