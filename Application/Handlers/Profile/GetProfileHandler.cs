using Application.Models;
using Application.Responses.Profile;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Profile;

public class GetProfileHandler
{
    private readonly IPermissionService _permissionService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;

    public GetProfileHandler(
        IPermissionService permissionService, 
        IJwtTokenService jwtTokenService, 
        IUserRepository userRepository)
    {
        _permissionService = permissionService;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }

    public async Task<GetProfileResponse> HandleAsync(
        string username, 
        string jwtToken,
        CancellationToken ct = default)
    {
        var claims = _jwtTokenService.ParseClaims(jwtToken); ;
        
        bool isOwner = _permissionService.IsProfileOwner(username!, claims);
        
        var user = await _userRepository.GetByUsernameAsync(username!, ct);

        var userDto = new ApplicationUserDto(user.Name, user.Surname, user.Patronymic, user.BirthDate);

        return new GetProfileResponse(userDto, isOwner);
    }
}