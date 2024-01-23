using Application.Exceptions.Common;
using Application.Models;
using Application.Requests.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Edu.Tasks;

public class CreateTaskHandler(
    IPermissionService permissionService,
    ITestTaskService testTaskService,
    ApplicationDbContext context)
{
    public async Task HandleAsync(CreateTaskRequest request, string jwtToken, CancellationToken ct = default)
    {
        var permissionResultUser = await permissionService
            .IsAdminOrCourseMentorAsync(jwtToken, request.CourseId, ct);
        if (permissionResultUser is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can create new tasks");
        }
        
        var course = await context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.CourseId, ct);
        NotFoundException.ThrowIfNull(course, nameof(course));

        await testTaskService.CreateTestTaskAsync(new CreateTestTaskModel
        {
            Author = permissionResultUser,
            Course = course!,
            TaskDto = request.TaskDto
        }, ct);
    }
}