namespace Application.Requests.Administration.Groups;

public record UpdateGroupRequest(
    Guid UpdatedGroupId,
    string NewTitle, 
    string NewDescription, 
    Guid NewDepartmentId);