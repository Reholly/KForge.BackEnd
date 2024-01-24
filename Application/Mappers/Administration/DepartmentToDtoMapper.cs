using Application.DTO.Administration;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers.Administration;

public class DepartmentToDtoMapper : IMapper<Department, DepartmentDto>
{
    public DepartmentDto Map(Department from)
    {
        return new DepartmentDto(from.Id, from.Title, from.Description, from.Groups
            .Select(g => new GroupDto(
                from.Id,
                g.Id,
                g.Title, 
                g.Description,
                g.Users
                    .Select(u => new ApplicationUserDto(
                        u.Name, 
                        u.Surname, 
                        u.Patronymic, 
                        u.BirthDate))
                    .ToArray()))
            .ToArray());
    }
    
}