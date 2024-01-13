using System.Security.Claims;
using Domain.Entities;

namespace Application.Services.Auth.Interfaces;

public interface IPermissionService
{ 
    bool IsProfileOwner(string profileOwnerUsername, Claim[] tokenClaims);
    bool IsCourseMentor(string[] courseMentors, Claim[] tokenClaims);
    bool IsInCourse(ApplicationUser user, Course course);
}