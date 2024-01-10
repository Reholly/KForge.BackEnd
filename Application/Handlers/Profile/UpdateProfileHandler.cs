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

    public async Task<UpdateProfileResponse> HandleAsync(
        AuthorizationWrapperRequest<UpdateProfileRequest> request, 
        RequestParametersModel? requestParameters,
        CancellationToken ct = default)
    {
        var claims = _jwtTokenService.ParseClaims(request.JwtToken);

        var username = requestParameters!.RouteParameters["username"].ToString();
        
        if (!_permissionService.IsProfileOwner(username!, claims))
            throw new PermissionDeniedException("Not profile owner.");
        
        var user = await _userRepository.GetByUsernameAsync(username!, ct);
        
        user.Name = request.Request.ApplicationUserModel.Name;
        user.Patronymic = request.Request.ApplicationUserModel.Patronymic;
        user.Surname = request.Request.ApplicationUserModel.Surname;
        user.BirthDate = request.Request.ApplicationUserModel.BirthDate;

        await _userRepository.UpdateUserAsync(user, ct);
        await _userRepository.CommitAsync(ct);
        
        return new UpdateProfileResponse();
    }
}