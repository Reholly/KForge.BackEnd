using Application.Models;
using Domain.Entities;

namespace Application.Services.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthTokensModel> LoginAsync(string username, string password);
    Task RegisterAsync(string username, string email, string password, ApplicationUser applicationUser, string role = "Student");
    Task ConfirmEmailAsync(string username, string code);
    Task ResetPasswordAsync(string username, string newPassword);
}