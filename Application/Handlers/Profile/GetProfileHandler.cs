using Application.Exceptions.Auth;
using Application.Requests.Profile;
using Application.Requests.Wrappers;
using Application.Responses.Profile;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Profile;

public class GetProfileHandler(
    IUserRepository userRepository, 
    IPermissionService permissionService,
    IJwtTokenService jwtTokenService)
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<GetProfileResponse> HandleAsync(
        AuthorizationWrapperRequest<GetProfileRequest> request,  
        CancellationToken ct = default)
    {
        var claims = _jwtTokenService.ParseClaims(request.JwtToken);

        if (!_permissionService.IsProfileOwner(request.Request.Username, claims))
            throw new PermissionDeniedException("Not profile owner.");
        
        var user = await _userRepository.GetByEmailAsync(request.Request.Username, ct);

        return new GetProfileResponse
        (
            user.Username,
            user.Name,
            user.Surname,
            user.Patronymic
        );
    }
}