using System.Security.Claims;
using Application.Exceptions.Common;
using Application.Services.Auth.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Edu.Tasks;

public class DeleteTaskHandler(
    IJwtTokenService jwtTokenService,
    IPermissionService permissionService,
    ApplicationDbContext context)
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
        var user = await context.Profiles
            .Include(au => au.CoursesAsMentor)
            .Include(au => au.CoursesAsStudent)
            .FirstOrDefaultAsync(au => au.Username == username, ct);
        NotFoundException.ThrowIfNull(user, nameof(user));
        
        var testTask = await context.TestTasks
            .FirstOrDefaultAsync(tt => tt.Id == taskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        if (!permissionService.IsCourseMentor(user!, testTask!.CourseId) 
            && tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can update tasks");
        }

        await Task.Run(() => context.TestTasks.Remove(testTask), ct);
        await context.SaveChangesAsync(ct);
    }
}