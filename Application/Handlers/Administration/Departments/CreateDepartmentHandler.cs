using Application.Requests.Administration.Department;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Application.Handlers.Administration.Departments;

public class CreateDepartmentHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task HandleAsync(CreateDepartmentRequest request, CancellationToken ct = default)
    {
        var department = new Department
        {
            Title = request.Title,
            Description = request.Description
        };

        await _dbContext.Departments.AddAsync(department, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}