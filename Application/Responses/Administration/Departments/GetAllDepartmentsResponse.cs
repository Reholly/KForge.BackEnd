using Application.DTO.Administration;

namespace Application.Responses.Administration.Departments;

public record GetAllDepartmentsResponse(DepartmentDto[] DepartmentsDto);