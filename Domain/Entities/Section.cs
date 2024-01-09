using Domain.Entities.Base;

namespace Domain.Entities;

public class Section : EntityBase
{
    public Guid CourseId { get; set; }
    
    public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}