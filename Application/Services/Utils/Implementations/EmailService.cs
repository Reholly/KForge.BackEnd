using Application.Options;
using Application.Services.Utils.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace Application.Services.Utils.Implementations;

public class EmailService(IOptions<EmailOptions> options)
    : IEmailService
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendEmailAsync(string email, string header, string message)
    {
        var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress(_options.From, _options.KForgeEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = header;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using var client = new SmtpClient();
        
        await client.ConnectAsync(_options.Host, _options.Port, true);
        await client.AuthenticateAsync(_options.KForgeEmail, _options.Password); 
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}