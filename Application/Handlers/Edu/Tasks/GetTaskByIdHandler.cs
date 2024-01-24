using Application.DTO.Edu;
using Application.Exceptions.Common;
using Application.Mappers;
using Application.Responses.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Edu.Tasks;

public class GetTaskByIdHandler(
    IJwtTokenService jwtTokenService,
    IPermissionService permissionService,
    IMapper<TestTask, TestTaskDto> testTaskMapper,
    ApplicationDbContext context)
{
    public async Task<GetTaskByIdResponse> HandleAsync(Guid taskId, string jwtToken,
        CancellationToken ct = default)
    {
        var testTask = await context.TestTasks
            .Include(testTask => testTask.Course)
            .FirstOrDefaultAsync(tt => tt.Id == taskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        var permissionResultUser = await permissionService
            .IsInCourseOrAdminAsync(jwtToken, testTask!.CourseId, ct);
        if (permissionResultUser is null)
        {
            throw new PermissionDeniedException($"User with username " +
                                                $"{jwtTokenService.GetUsernameFromAccessToken(jwtToken)} " +
                                                $"is not in course {testTask.Course!.Title}");
        }
        
        return new GetTaskByIdResponse
        {
            TestTask = testTaskMapper.Map(testTask)
        };
    }
}