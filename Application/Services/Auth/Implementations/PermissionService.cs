using System.Security.Claims;
using Application.Exceptions.Common;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Auth.Implementations;

public class PermissionService(
        IJwtTokenService tokenService,
        ApplicationDbContext context)
    : IPermissionService
{
    private readonly IJwtTokenService _tokenService = tokenService;

    public bool IsProfileOwner(string profileOwnerUsername, string jwtToken)
    {
        var claims = _tokenService.ParseClaims(jwtToken);

        return claims.FirstOrDefault(x => x.Value == profileOwnerUsername) is not null;
    }

    public bool IsCourseMentor(ApplicationUser user, Guid courseId)
        => user.CoursesAsMentor.FirstOrDefault(c => c.Id == courseId) is not null;

    public bool IsInCourse(ApplicationUser user, Guid courseId)
        => user.CoursesAsMentor.FirstOrDefault(c => c.Id == courseId) is not null
           || user.CoursesAsStudent.FirstOrDefault(c => c.Id == courseId) is not null;

    public async Task<ApplicationUser?> 
        IsAdminOrCourseMentorAsync(string jwtToken, Guid courseId, CancellationToken ct = default)
    {
        var tokenClaims = _tokenService.ParseClaims(jwtToken);
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

        return IsCourseMentor(user!, courseId) || tokenClaims.FirstOrDefault(c =>
            c is { Type: ClaimTypes.Role, Value: "Admin" }) is not null
            ? user
            : null;
    }

    public async Task<ApplicationUser?> 
        IsInCourseOrAdminAsync(string jwtToken, Guid courseId, CancellationToken ct = default)
    {
        var tokenClaims = _tokenService.ParseClaims(jwtToken);
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

        return tokenClaims.FirstOrDefault(c =>
                   c is { Type: ClaimTypes.Role, Value: "Admin" }) is not null
               || IsInCourse(user!, courseId)
            ? user
            : null;
    }
}