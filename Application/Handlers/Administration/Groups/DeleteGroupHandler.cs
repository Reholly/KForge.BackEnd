using Application.Exceptions.Common;
using Application.Requests.Administration.Groups;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Groups;

public class DeleteGroupHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task HandleAsync(DeleteGroupRequest request, CancellationToken ct = default)
    {
        var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == request.GroupId, ct)
            ?? throw new NotFoundException("Group not found.");
        
        await Task.Run(() => _dbContext.Groups.Remove(group), ct);

        await _dbContext.SaveChangesAsync(ct);
    }
}