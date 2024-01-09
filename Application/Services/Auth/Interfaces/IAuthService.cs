using Application.Models;

namespace Application.Services.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthTokensModel> LoginAsync(string email, string password);
    Task RegisterAsync(string email, string password, string role = "Student");
}