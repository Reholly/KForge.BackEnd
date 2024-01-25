using Domain.Entities.Base;

namespace Domain.Entities;

public class Tag : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public ICollection<Course> Courses = new List<Course>();
}