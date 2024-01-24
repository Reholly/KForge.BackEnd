namespace Application.Requests.Administration.Groups;

public record CreateGroupRequest(
    Guid DepartmentId, 
    string Title, 
    string Description);