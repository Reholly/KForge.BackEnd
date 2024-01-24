using Application.Handlers.Administration.Departments;
using Application.Requests.Administration.Department;
using Application.Responses.Administration.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Administration;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/departments")]
public class DepartmentsController
{
    [HttpGet]
    public Task<GetAllDepartmentsResponse> GetAllDepartments(
        [FromServices] GetAllDepartmentsHandler handler,
        CancellationToken ct = default) 
        => handler.HandleAsync(ct);
    
    [HttpGet("{id}")]
    public Task<GetDepartmentByIdResponse> GetDepartmentById(
        [FromServices] GetDepartmentByIdHandler handler,
        [FromRoute] Guid id,
        CancellationToken ct = default)
        => handler.HandleAsync(id, ct);
    
    [HttpPut("{id}")]
    public Task UpdateDepartment(
        [FromServices] UpdateDepartmentHandler handler,
        [FromRoute] Guid id,
        [FromBody] UpdateDepartmentRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(id,request, ct);
    
    [HttpDelete("{id}")]
    public Task DeleteDepartment(
        [FromServices] DeleteDepartmentHandler handler,
        [FromRoute] Guid id,
        CancellationToken ct = default)
        => handler.HandleAsync(id, ct);
    
    [HttpPost]
    public Task CreateDepartment(
        [FromServices] CreateDepartmentHandler handler,
        [FromBody] CreateDepartmentRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
}