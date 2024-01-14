using Application.Services.Auth.Interfaces;

namespace Application.Handlers.Auth;

public class EmailConfirmHandler
{
    private readonly IAuthService _authService;

    public EmailConfirmHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task HandleAsync(string username, string code, CancellationToken ct = default)
    {
        await _authService.ConfirmEmailAsync(username, code);
    }
}