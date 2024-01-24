using Application.DTO.Administration;
using Application.Exceptions.Common;
using Application.Mappers;
using Application.Requests.Administration.Groups;
using Application.Responses.Administration.Groups;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Groups;

public class GetGroupWithUsersHandler(
    ApplicationDbContext dbContext, 
    IMapper<Group, GroupDto> mapper)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper<Group, GroupDto> _mapper = mapper;

    public async Task<GetGroupWithUsersResponse> HandleAsync(GetGroupWithUsersRequest request, CancellationToken ct = default)
    {
        var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == request.GroupId, cancellationToken: ct) 
                    ?? throw new NotFoundException("Group not found.");
        
        return new GetGroupWithUsersResponse(_mapper.Map(group));
    }
}