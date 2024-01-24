using Application.Exceptions.Common;
using Application.Requests.Administration.Department;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Departments;

public class UpdateDepartmentHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task HandleAsync(Guid id, UpdateDepartmentRequest request, CancellationToken ct = default)
    {
        var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id, ct)
                         ?? throw new NotFoundException("Department not found");
        department.Description = request.Description;
        department.Title = request.Title;

        await Task.Run(() => _dbContext.Departments.Update(department), ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}