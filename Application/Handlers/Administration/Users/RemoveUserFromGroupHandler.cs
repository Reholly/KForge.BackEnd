using Application.Exceptions.Common;
using Application.Requests.Administration.Users;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Users;

public class RemoveUserFromGroupHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task HandleAsync(RemoveUserFromGroupRequest request, CancellationToken ct = default)
    {
        var group = await _dbContext.Groups
            .FirstOrDefaultAsync(x => x.Id == request.UserWithGroupDto.Group, cancellationToken: ct);
        var user = await _dbContext.Profiles
            .FirstOrDefaultAsync(x => x.Username == request.UserWithGroupDto.Username, cancellationToken: ct);
        
        if (group is null)
            throw new NotFoundException("Group not found.");
        if (user is null)
            throw new NotFoundException("User not found.");

        user.Groups.Remove(group);
        group.Users.Remove(user);

        await _dbContext.SaveChangesAsync(ct);
    }
}