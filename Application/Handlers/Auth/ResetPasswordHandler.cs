using Application.DTO.Security;
using Application.Exceptions.Common;
using Application.Requests.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;

namespace Application.Handlers.Auth;

public class ResetPasswordHandler(
    IPermissionService permissionService,
    IValidator<ResetPasswordDto> validator,
    ISecurityService securityService,
    IJwtTokenService tokenService)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IValidator<ResetPasswordDto> _validator = validator;
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IJwtTokenService _tokenService = tokenService;

    public async Task HandleAsync(ResetPasswordRequest request, string jwtToken, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(request.ResetPasswordDto, ct);
        
        if (!_permissionService.IsProfileOwner(request.ResetPasswordDto.Username, jwtToken))
            throw new PermissionDeniedException("Password reset failed: not an owner.");

        await _securityService.ResetPasswordAsync(request.ResetPasswordDto);
    }
}