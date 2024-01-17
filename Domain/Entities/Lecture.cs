using Domain.Entities.Base;

namespace Domain.Entities;

public class Lecture : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    
    public Guid CourseId { get; set; }
    public Guid SectionId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? EditAt { get; set; } = default;
    
    public Guid AuthorId { get; set; }
}