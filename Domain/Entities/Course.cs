using Domain.Entities.Base;

namespace Domain.Entities;

public class Course : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AcademicHoursDuration { get; set; }
    
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    
    public ICollection<ApplicationUser> Students { get; set; } = new List<ApplicationUser>();
    public ICollection<ApplicationUser> Mentors { get; set; } = new List<ApplicationUser>();

    public ICollection<Section> Sections { get; set; } = new List<Section>();
    public ICollection<TestTask>? TestTasks { get; set; }
}