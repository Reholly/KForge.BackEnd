using Application.Exceptions.Common;
using Application.Requests.Administration.Groups;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Groups;

public class UpdateGroupHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task HandleAsync(UpdateGroupRequest request, CancellationToken ct = default)
    {
        var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == request.UpdatedGroupId, cancellationToken: ct) ??
                    throw new NotFoundException("Group not found.");
        
        group.DepartmentId = request.NewDepartmentId;
        group.Description = request.NewDescription;
        group.Title = request.NewTitle;

        await Task.Run(() =>_dbContext.Groups.Update(group), ct);

        await _dbContext.SaveChangesAsync(ct);
    }
}