using Application.Models;
using Application.Requests.Profile;
using Application.Responses.Profile;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Application.Handlers.Profile;

public class GetProfileHandler(
    IPermissionService permissionService,
    IJwtTokenService jwtTokenService,
    IValidator<GetProfileRequest> validator,
    IUserRepository userRepository)
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<GetProfileRequest> _validator = validator;

    public async Task<GetProfileResponse> HandleAsync(
        GetProfileRequest request, 
        string jwtToken,
        CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        var claims = _jwtTokenService.ParseClaims(jwtToken); ;
        
        bool isOwner = _permissionService.IsProfileOwner(request.Username, claims);
        
        var user = await _userRepository.GetByUsernameAsync(request.Username, ct);

        var userDto = new ApplicationUserDto(user.Name, user.Surname, user.Patronymic, user.BirthDate);

        return new GetProfileResponse(userDto, isOwner);
    }
}