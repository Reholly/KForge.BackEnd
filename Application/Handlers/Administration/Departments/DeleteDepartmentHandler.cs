using Application.Exceptions.Common;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Departments;

public class DeleteDepartmentHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task HandleAsync(Guid id, CancellationToken ct = default)
    {
        var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id, ct)
                         ?? throw new NotFoundException("Department not found");
        
        await Task.Run(() =>_dbContext.Departments.Remove(department), ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}