using Domain.Entities.Base;

namespace Domain.Entities;

public class Department : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public ICollection<Group> Groups { get; set; } = new List<Group>();
}