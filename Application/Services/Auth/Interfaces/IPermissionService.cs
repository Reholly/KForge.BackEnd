using Domain.Entities;

namespace Application.Services.Auth.Interfaces;

public interface IPermissionService
{
    bool IsProfileOwner(string profileOwnerUsername, string jwtToken);
    bool IsCourseMentor(ApplicationUser user, Guid courseId);
    bool IsInCourse(ApplicationUser user, Guid courseId);

    Task<ApplicationUser?>
        IsAdminOrCourseMentorAsync(string jwtToken, Guid courseId, CancellationToken ct = default);

    Task<ApplicationUser?>
        IsInCourseOrAdminAsync(string jwtToken, Guid courseId, CancellationToken ct = default);
}