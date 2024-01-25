using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.User;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/user/stats")]
public class UserStatsController
{
    
}