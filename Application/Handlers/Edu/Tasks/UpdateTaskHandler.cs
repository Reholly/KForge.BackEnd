using Application.DTO.Edu;
using Application.Exceptions.Common;
using Application.Mappers;
using Application.Requests.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Edu.Tasks;

public class UpdateTaskHandler(
    IPermissionService permissionService,
    IMapper<TestTask, TestTaskDto> testTaskMapper,
    ApplicationDbContext context)
{
    public async Task HandleAsync(UpdateTaskRequest request, string jwtToken, 
        CancellationToken ct = default)
    {
        var testTask = await context.TestTasks
            .FirstOrDefaultAsync(tt => tt.Id == request.TaskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));
        
        var permissionResultUser = await permissionService
            .IsAdminOrCourseMentorAsync(jwtToken, testTask!.CourseId, ct);
        if (permissionResultUser is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can update tasks");
        }

        testTask = testTaskMapper.MapReverse(testTask, request.TaskDto);
        
        await Task.Run(() => context.TestTasks.Update(testTask), ct);
        await context.SaveChangesAsync(ct);
    }
}