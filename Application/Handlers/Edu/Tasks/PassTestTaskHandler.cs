using System.Security.Claims;
using Application.DTO.Edu;
using Application.Exceptions;
using Application.Exceptions.Auth;
using Application.Mappers;
using Application.Requests.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Edu.Tasks;

public class PassTestTaskHandler(
    IJwtTokenService jwtTokenService,
    IUserRepository userRepository,
    ITestTaskRepository testTaskRepository,
    IPermissionService permissionService,
    ITestTaskService testTaskService,
    IMapper<TestTaskResult, TestTaskResultDto> testTaskResultMapper)
{
    public async Task<TestTaskResultDto> HandleAsync(PassTestTaskRequest request, string jwtToken, 
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

        var testTask = await testTaskRepository.GetTaskByIdAsync(request.TaskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        if (tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is not null)
        {
            var result1 = await testTaskService
                .PassTestTaskAsync(testTask!, request.AnsweredQuestions, user!, ct);
            return testTaskResultMapper.Map(result1);
        }

        if (!permissionService.IsInCourse(user!, testTask!.CourseId))
        {
            throw new PermissionDeniedException($"User with username {username} " +
                                                $"is not in course {testTask.Course!.Title}");
        }
        
        var result = await testTaskService
            .PassTestTaskAsync(testTask, request.AnsweredQuestions, user!, ct);
        return testTaskResultMapper.Map(result);
    }
}