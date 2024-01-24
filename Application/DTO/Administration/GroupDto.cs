using Application.Models;

namespace Application.DTO.Administration;

public record GroupDto(
    Guid DepartmentId,
    Guid GroupId,
    string Title,
    string Description, 
    ApplicationUserDto[] Users);