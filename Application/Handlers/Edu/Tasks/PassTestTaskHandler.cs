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
        var testTask = await context.TestTasks
            .Include(testTask => testTask.Course)
            .FirstOrDefaultAsync(tt => tt.Id == request.TaskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        var permissionResultUser = await permissionService
            .IsInCourseOrAdminAsync(jwtToken, testTask!.CourseId, ct);

        if (permissionResultUser is null)
        {
            throw new PermissionDeniedException($"User with username " +
                                                $"{jwtTokenService.GetUsernameFromAccessToken(jwtToken)} " +
                                                $"is not in course {testTask.Course!.Title}");
        }
        
        var result = await testTaskService
            .PassTestTaskAsync(testTask, request.AnsweredQuestions, permissionResultUser, ct);
        return new PassTestTaskResponse
        {
            Result = testTaskResultMapper.Map(result)
        };
    }
}