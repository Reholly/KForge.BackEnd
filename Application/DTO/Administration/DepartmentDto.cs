namespace Application.DTO.Administration;

public record DepartmentDto(
    Guid Id,
    string Title, 
    string Description, 
    IEnumerable<GroupDto> Groups);
