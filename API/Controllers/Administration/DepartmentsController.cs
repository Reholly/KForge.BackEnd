using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Administration;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/departments")]
public class DepartmentsController
{
    
}