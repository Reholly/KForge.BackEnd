using Domain.Entities.Base;

namespace Domain.Entities;

public class Section : EntityBase
{
    public string Title { get; set; } = string.Empty;
    
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    
    
    public int OrdinalInCourse { get; set; }
    
    public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    public ICollection<TestTask> Tasks { get; set; } = new List<TestTask>();
}