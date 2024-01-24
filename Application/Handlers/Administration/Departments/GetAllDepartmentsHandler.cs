using Application.DTO.Administration;
using Application.Mappers;
using Application.Responses.Administration.Departments;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Administration.Departments;

public class GetAllDepartmentsHandler(
    ApplicationDbContext dbContext, 
    IMapper<Department, DepartmentDto> mapper)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper<Department, DepartmentDto> _mapper = mapper;

    public async Task<GetAllDepartmentsResponse> HandleAsync(CancellationToken ct = default)
    {
        var departments = await _dbContext.Departments
            .Include(x => x.Groups)
            .ThenInclude(x => x.Users)
            .ToArrayAsync(ct);

        return new GetAllDepartmentsResponse(departments
            .Select(d => _mapper.Map(d))
            .ToArray());
    }
}