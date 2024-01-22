using System.Security.Claims;
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
    IJwtTokenService jwtTokenService,
    IPermissionService permissionService,
    IMapper<TestTask, TestTaskDto> testTaskMapper,
    ApplicationDbContext context)
{
    public async Task HandleAsync(UpdateTaskRequest request, string jwtToken, 
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

        if (!permissionService.IsCourseMentor(user!, testTask!.CourseId) 
            && tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can update tasks");
        }

        testTask = testTaskMapper.MapReverse(testTask, request.TaskDto);
        
        await Task.Run(() => context.TestTasks.Update(testTask), ct);
        await context.SaveChangesAsync(ct);
    }
}