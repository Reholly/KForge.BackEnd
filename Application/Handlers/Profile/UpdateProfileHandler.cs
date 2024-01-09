using Application.Exceptions.Auth;
using Application.Requests.Profile;
using Application.Requests.Wrappers;
using Application.Responses.Profile;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Profile;

public class UpdateProfileHandler(
    IUserRepository userRepository,
    IPermissionService permissionService,
    IJwtTokenService jwtTokenService)
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UpdateProfileResponse> HandleAsync(
        AuthorizationWrapperRequest<UpdateProfileRequest> request, 
        CancellationToken ct = default)
    {
        var claims = _jwtTokenService.ParseClaims(request.JwtToken);
        
        if (!_permissionService.IsProfileOwner(request.Request.Username, claims))
            throw new PermissionDeniedException("Not profile owner.");
        
        var user = await _userRepository.GetByUsernameAsync(request.Request.Username, ct);
        
        user.Name = request.Request.ApplicationUserModel.Name;
        user.Patronymic = request.Request.ApplicationUserModel.Patronymic;
        user.Surname = request.Request.ApplicationUserModel.Surname;
        user.BirthDate = request.Request.ApplicationUserModel.BirthDate;

        await _userRepository.UpdateUserAsync(user, ct);
        await _userRepository.CommitAsync(ct);
        
        return new UpdateProfileResponse();
    }
}