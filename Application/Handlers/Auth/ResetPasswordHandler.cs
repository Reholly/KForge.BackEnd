using Application.DTO.Security;
using Application.Exceptions.Common;
using Application.Requests.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;

namespace Application.Handlers.Auth;

public class ResetPasswordHandler(
    IPermissionService permissionService,
    IValidator<ResetPasswordDto> validator,
    IAuthService authService,
    IJwtTokenService tokenService)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<ResetPasswordDto> _validator = validator;
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IJwtTokenService _tokenService = tokenService;

    public async Task HandleAsync(ResetPasswordDto dto, string jwtToken, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(dto, ct);
        
        if (!_permissionService.IsProfileOwner(dto.Username, _tokenService.ParseClaims(jwtToken)))
            throw new PermissionDeniedException("Password reset failed: not an owner.");

        await _authService.ResetPasswordAsync(dto.Username, dto.NewPassword);
    }
}