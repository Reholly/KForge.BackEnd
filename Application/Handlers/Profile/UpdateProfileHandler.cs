using Application.Exceptions.Common;
using Application.Requests.Profile;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Profile;

public class UpdateProfileHandler
{
    private readonly IPermissionService _permissionService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;

    public UpdateProfileHandler(
        IPermissionService permissionService, 
        IJwtTokenService jwtTokenService, 
        IUserRepository userRepository)
    {
        _permissionService = permissionService;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(
        UpdateProfileRequest request,
        string jwtToken,
        CancellationToken ct = default)
    {
        var claims = _jwtTokenService.ParseClaims(jwtToken);

        
        if (!_permissionService.IsProfileOwner(request.ProfileUsername, claims))
            throw new PermissionDeniedException("Not profile owner.");
        
        var user = await _userRepository.GetByUsernameAsync(request.ProfileUsername, ct);
        
        user.Name = request.ApplicationUserDto.Name;
        user.Patronymic = request.ApplicationUserDto.Patronymic;
        user.Surname = request.ApplicationUserDto.Surname;
        user.BirthDate = request.ApplicationUserDto.BirthDate;

        await _userRepository.UpdateUserAsync(user, ct);
        await _userRepository.CommitAsync(ct);
    }
}