using Application.Services.Utils.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Utils.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress("Admin", _configuration["EmailNotification:KForgeEmail"]));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using var client = new SmtpClient();
        
        await client.ConnectAsync(
            _configuration["EmailNotification:Host"], 
            int.Parse(_configuration["EmailNotification:Port"]!), 
            true);
        await client.AuthenticateAsync(
            _configuration["EmailNotification:KForgeEmail"], 
            _configuration["EmailNotification:EmailPassword"]);
        await client.SendAsync(emailMessage);
 
        await client.DisconnectAsync(true);
    }
}