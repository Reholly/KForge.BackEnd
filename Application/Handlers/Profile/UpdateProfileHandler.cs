using Application.Exceptions.Auth;
using Application.Models;
using Application.Requests.Profile;
using Application.Requests.Wrappers;
using Application.Responses.Profile;
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
        string usernameFromRoute,
        string jwtToken,
        CancellationToken ct = default)
    {
        var claims = _jwtTokenService.ParseClaims(jwtToken);

        
        if (!_permissionService.IsProfileOwner(usernameFromRoute, claims))
            throw new PermissionDeniedException("Not profile owner.");
        
        var user = await _userRepository.GetByUsernameAsync(usernameFromRoute, ct);
        
        user.Name = request.UserModel.Name;
        user.Patronymic = request.UserModel.Patronymic;
        user.Surname = request.UserModel.Surname;
        user.BirthDate = request.UserModel.BirthDate;

        await _userRepository.UpdateUserAsync(user, ct);
        await _userRepository.CommitAsync(ct);
    }
}