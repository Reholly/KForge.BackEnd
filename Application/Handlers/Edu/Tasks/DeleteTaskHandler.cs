using Application.Exceptions.Common;
using Application.Services.Auth.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Edu.Tasks;

public class DeleteTaskHandler(
    IPermissionService permissionService,
    ApplicationDbContext context)
{
    public async Task HandleAsync(Guid taskId, string jwtToken, 
        CancellationToken ct = default)
    {
        var testTask = await context.TestTasks
            .FirstOrDefaultAsync(tt => tt.Id == taskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));
        
        var permissionResultUser = await permissionService
            .IsAdminOrCourseMentorAsync(jwtToken, testTask!.CourseId, ct);
        if (permissionResultUser is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can delete tasks");
        }

        await Task.Run(() => context.TestTasks.Remove(testTask), ct);
        await context.SaveChangesAsync(ct);
    }
}