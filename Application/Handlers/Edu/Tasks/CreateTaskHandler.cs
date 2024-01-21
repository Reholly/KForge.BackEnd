using System.Security.Claims;
using Application.Exceptions.Common;
using Application.Models;
using Application.Requests.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Interfaces;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Edu.Tasks;

public class CreateTaskHandler(
    IJwtTokenService jwtTokenService,
    IUserRepository userRepository,
    IPermissionService permissionService,
    ICourseRepository courseRepository,
    ITestTaskService testTaskService)
{
    public async Task HandleAsync(CreateTaskRequest request, string jwtToken, CancellationToken ct = default)
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
        
        if (!permissionService.IsCourseMentor(user!, request.CourseId) 
            && tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can create new tasks");
        }

        var course = await courseRepository.GetCourseByIdAsync(request.CourseId, ct);
        NotFoundException.ThrowIfNull(course, nameof(course));

        await testTaskService.CreateTestTaskAsync(new CreateTestTaskModel
        {
            Author = user!,
            Course = course!,
            TaskDto = request.TaskDto
        }, ct);
    }
}