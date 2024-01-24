using Application.DTO.Administration;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers.Administration;

public class GroupToDtoMapper : IMapper<Group, GroupDto>
{
    public GroupDto Map(Group from)
    {
        return new GroupDto(
            from.DepartmentId, 
            from.Id, 
            from.Title, 
            from.Description,
            from.Users.Select(u => new ApplicationUserDto(
                u.Name, 
                u.Surname,
                u.Patronymic,
                u.BirthDate)));
    }
}