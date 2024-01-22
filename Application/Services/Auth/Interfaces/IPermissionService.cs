using System.Security.Claims;
using Domain.Entities;

namespace Application.Services.Auth.Interfaces;

public interface IPermissionService
{ 
    bool IsProfileOwner(string profileOwnerUsername, string jwtToken);
    bool IsCourseMentor(ApplicationUser user, Guid courseId);
    bool IsInCourse(ApplicationUser user, Guid courseId);
}