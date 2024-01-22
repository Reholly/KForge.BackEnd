using Application.Exceptions.Common;
using Application.Requests.Profile;
using Application.Services.Auth.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Profile;

public class UpdateProfileHandler(
    IPermissionService permissionService,
    ApplicationDbContext dbContext)
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task HandleAsync(
        UpdateProfileRequest request,
        string jwtToken,
        CancellationToken ct = default)
    {
        if (!_permissionService.IsProfileOwner(request.ProfileUsername, jwtToken))
            throw new PermissionDeniedException("Not profile owner.");

        var user = await _dbContext.Profiles
                       .FirstOrDefaultAsync(x => x.Username == request.ProfileUsername, ct) 
                   ?? throw new NotFoundException("User not found.");
        
        user.Name = request.ApplicationUserDto.Name;
        user.Patronymic = request.ApplicationUserDto.Patronymic;
        user.Surname = request.ApplicationUserDto.Surname;
        user.BirthDate = request.ApplicationUserDto.BirthDate;

        await Task.Run(() => _dbContext.Profiles.Update(user), ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}