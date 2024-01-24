using Domain.Entities.Base;

namespace Domain.Entities;

public class Group : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}