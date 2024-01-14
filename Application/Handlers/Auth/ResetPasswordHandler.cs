using Application.Exceptions.Auth;
using Application.Requests.Auth;
using Application.Services.Auth.Interfaces;
using FluentValidation;

namespace Application.Handlers.Auth;

public class ResetPasswordHandler(
    IPermissionService permissionService,
    IValidator<ResetPasswordRequest> validator,
    IAuthService authService,
    IJwtTokenService tokenService)
{
    private readonly IAuthService _authService = authService;
    private readonly IValidator<ResetPasswordRequest> _validator = validator;
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IJwtTokenService _tokenService = tokenService;

    public async Task HandleAsync(ResetPasswordRequest request, string jwtToken, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        if (!_permissionService.IsProfileOwner(request.Username, _tokenService.ParseClaims(jwtToken)))
            throw new PermissionDeniedException("Password reset failed: not an owner.");

        await _authService.ResetPasswordAsync(request.Username, request.NewPassword);
    }
}