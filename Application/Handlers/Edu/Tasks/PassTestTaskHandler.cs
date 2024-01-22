using System.Security.Claims;
using Application.DTO.Edu;
using Application.Exceptions.Common;
using Application.Mappers;
using Application.Requests.Education.Tasks;
using Application.Responses.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Interfaces;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Edu.Tasks;

public class PassTestTaskHandler(
    IJwtTokenService jwtTokenService,
    IPermissionService permissionService,
    ITestTaskService testTaskService,
    IMapper<TestTaskResult, TestTaskResultDto> testTaskResultMapper,
    ApplicationDbContext context)
{
    public async Task<PassTestTaskResponse> HandleAsync(PassTestTaskRequest request, string jwtToken, 
        CancellationToken ct = default)
    {
        var tokenClaims = jwtTokenService.ParseClaims(jwtToken);
        var usernameClaim = tokenClaims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
        if (usernameClaim is null)
        {
            throw new PermissionDeniedException("Token doesn't contain username");
        }

        string username = usernameClaim.Value;
        var user = await context.Profiles
            .Include(au => au.CoursesAsMentor)
            .Include(au => au.CoursesAsStudent)
            .FirstOrDefaultAsync(au => au.Username == username, ct);
        NotFoundException.ThrowIfNull(user, nameof(user));

        var testTask = await context.TestTasks
            .Include(testTask => testTask.Course)
            .FirstOrDefaultAsync(tt => tt.Id == request.TaskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        if (tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is not null)
        {
            var result1 = await testTaskService
                .PassTestTaskAsync(testTask!, request.AnsweredQuestions, user!, ct);
            return new PassTestTaskResponse
            {
                Result = testTaskResultMapper.Map(result1)
            };
        }

        if (!permissionService.IsInCourse(user!, testTask!.CourseId))
        {
            throw new PermissionDeniedException($"User with username {username} " +
                                                $"is not in course {testTask.Course!.Title}");
        }
        
        var result = await testTaskService
            .PassTestTaskAsync(testTask, request.AnsweredQuestions, user!, ct);
        return new PassTestTaskResponse
        {
            Result = testTaskResultMapper.Map(result)
        };
    }
}