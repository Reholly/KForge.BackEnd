using System.Security.Claims;
using Application.DTO.Edu;
using Application.Exceptions;
using Application.Exceptions.Auth;
using Application.Mappers;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Edu.Tasks;

public class GetTaskByIdHandler(
    IJwtTokenService jwtTokenService,
    IUserRepository userRepository,
    ITestTaskRepository testTaskRepository,
    IPermissionService permissionService,
    IMapper<TestTask, TestTaskDto> testTaskMapper)
{
    public async Task<TestTaskDto> HandleAsync(Guid taskId, string jwtToken,
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

        if (tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is not null)
        {
            return testTaskMapper.Map(testTask!);
        }

        if (!permissionService.IsInCourse(user!, testTask!.CourseId))
        {
            throw new PermissionDeniedException($"User with username {username} " +
                                                $"is not in course {testTask.Course!.Title}");
        }
        
        return testTaskMapper.Map(testTask);
    }
}