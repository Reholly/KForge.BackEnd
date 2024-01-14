using System.Security.Claims;
using Domain.Entities;

namespace Application.Services.Auth.Interfaces;

public interface IPermissionService
{ 
    bool IsProfileOwner(string profileOwnerUsername, Claim[] tokenClaims);
    bool IsCourseMentor(ApplicationUser user, Guid courseId);
    bool IsInCourse(ApplicationUser user, Guid courseId);
}