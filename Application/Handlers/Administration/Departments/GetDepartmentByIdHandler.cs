using Application.DTO.Administration;
using Application.Exceptions.Common;
using Application.Mappers;
using Application.Responses.Administration.Departments;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Departments;

public class GetDepartmentByIdHandler(
    ApplicationDbContext dbContext, 
    IMapper<Department, DepartmentDto> mapper)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper<Department, DepartmentDto> _mapper = mapper;

    public async Task<GetDepartmentByIdResponse> HandleAsync(Guid id, CancellationToken ct = default)
    {
        var department = await _dbContext.Departments
                             .Include(x => x.Groups)
                             .ThenInclude(x => x.Users)
                             .FirstOrDefaultAsync(x => x.Id == id, ct)
                         ?? throw new NotFoundException("Department not found.");

        return new GetDepartmentByIdResponse(_mapper.Map(department));
    }
}