using System.Security.Claims;
using Application.Exceptions;
using Application.Exceptions.Auth;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Edu.Tasks;

public class DeleteTaskHandler(
    IJwtTokenService jwtTokenService,
    IUserRepository userRepository,
    ITestTaskRepository testTaskRepository,
    IPermissionService permissionService)
{
    public async Task HandleAsync(Guid taskId, string jwtToken, 
        CancellationToken ct = default)
    {
        var tokenClaims = jwtTokenService.ParseClaims(jwtToken);
        var usernameClaim = tokenClaims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
        if (usernameClaim is null)
        {
            throw new PermissionDeniedException("Token doesn't contain username");
        }

        string username = usernameClaim.Value;
        var user = await userRepository.GetByUsernameWithCoursesAsync(username, ct);
        NotFoundException.ThrowIfNull(user, nameof(user));

        var testTask = await testTaskRepository.GetTaskByIdAsync(taskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        if (!permissionService.IsCourseMentor(user!, testTask!.CourseId) 
            && tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can update tasks");
        }

        await testTaskRepository.DeleteTaskAsync(testTask, ct);
        await testTaskRepository.CommitChangesAsync(ct);
    }
}