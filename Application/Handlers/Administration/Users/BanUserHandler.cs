using Application.Exceptions.Common;
using Application.Requests.Administration.Users;
using Application.Services.Auth.Interfaces;
using Application.Services.Utils.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.Administration.Users;

public class BanUserHandler(
    IEmailService emailService,
    IJwtTokenStorage storage,
    UserManager<IdentityUser> userManager)
{
    private readonly IEmailService _emailService = emailService;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IJwtTokenStorage _storage = storage;

    public async Task HandleAsync(BanUserRequest request, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
            throw new NotFoundException("User not found.");

        user.LockoutEnd = DateTime.UtcNow.AddDays(request.Days);
        await _userManager.UpdateAsync(user);
        
        _storage.AddToBlackList(request.Username, request.Days);

        await _emailService.SendEmailAsync(user.Email!, "Бан",
            $"Вы были забанены на платформе KForge на : {request.Days} дней по причине : {request.Reason}" +
            $" Если это произошло по ошибке, просьба связаться с тех.поддержкой сайта.");
    }
}