using Application.Exceptions.Common;
using Application.Requests.Administration.Groups;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Groups;

public class CreateGroupHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task HandleAsync(CreateGroupRequest request, CancellationToken ct = default)
    {
        var group = new Group
        {
            Title = request.Title,
            Description = request.Description,
            DepartmentId = request.DepartmentId
        };
        
        await _dbContext.Groups.AddAsync(group, ct);
        
        await _dbContext.SaveChangesAsync(ct);
    }
}