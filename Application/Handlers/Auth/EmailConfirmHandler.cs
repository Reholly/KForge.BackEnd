using Application.DTO.Auth;
using Application.Services.Auth.Interfaces;

namespace Application.Handlers.Auth;

public class EmailConfirmHandler(IRegistrationService registrationService)
{
    private readonly IRegistrationService _registrationService = registrationService;

    public async Task HandleAsync(ConfirmEmailDto dto, CancellationToken ct = default)
    {
        await _registrationService.ConfirmEmailAsync(dto);
    }
}