using System.Security.Claims;
using Application.Services.Auth.Interfaces;
using Domain.Entities;

namespace Application.Services.Auth.Implementations;

public class PermissionService : IPermissionService
{
    public bool IsProfileOwner(string profileOwnerUsername, Claim[] tokenClaims) 
        => tokenClaims.FirstOrDefault(x => x.Value == profileOwnerUsername) is not null;

    public bool IsCourseMentor(ApplicationUser user, Guid courseId)
        => user.CoursesAsMentor.FirstOrDefault(c => c.Id == courseId) is not null;

    public bool IsInCourse(ApplicationUser user, Guid courseId)
        => user.CoursesAsMentor.FirstOrDefault(c => c.Id == courseId) is not null
           || user.CoursesAsStudent.FirstOrDefault(c => c.Id == courseId) is not null;
}