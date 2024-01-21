namespace Application.Services.Utils.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string email, string header, string message);
}