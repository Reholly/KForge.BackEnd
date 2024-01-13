using System.Security.Claims;
using Application.Exceptions.Auth;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Edu.Tasks;

public class GetTaskByIdHandler(
    IJwtTokenService jwtTokenService, 
    IUserRepository userRepository)
{
    public async Task HandleAsync(Guid taskId, string jwtToken, 
        CancellationToken ct = default)
    {
        var tokenClaims = jwtTokenService.ParseClaims(jwtToken);
        var usernameClaim = tokenClaims.FirstOrDefault(c => c.Type != ClaimTypes.UserData);
        if (usernameClaim is null)
        {
            throw new PermissionDeniedException("Token doesn't contain username");
        }

        string username = usernameClaim.Value;
        var user = await userRepository.GetByUsernameAsync(username, ct);
    }
}