using Application.Exceptions.Common;
using Application.Requests.Administration.Users;
using Application.Services.Auth.Interfaces;
using Application.Services.Utils.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.Administration.Users;

public class UnbanUserHandler(
    IEmailService emailService,
    IJwtTokenStorage storage,
    UserManager<IdentityUser> userManager)
{
    private readonly IEmailService _emailService = emailService;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IJwtTokenStorage _storage = storage;

    public async Task HandleAsync(UnbanUserRequest request, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
            throw new NotFoundException("User not found.");

        user.LockoutEnd = null;
        await _userManager.UpdateAsync(user);
        
        _storage.RemoveFromBlackList(request.Username);

        await _emailService.SendEmailAsync(user.Email!, "Разбан",
            $"Вы были разбанены на платформе KForge.");
    }
}