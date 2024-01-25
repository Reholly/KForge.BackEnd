using Domain.Entities.Base;

namespace Domain.Entities;

public class TestTask : EntityBase
{
    public int OrdinalInSection { get; set; }
    
    public required string Title { get; set; }
    
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public Guid SectionId { get; set; }
    public Section Section { get; set; }
    
    public ICollection<Question>? Questions { get; set; }
    
    public required Guid AuthorId { get; set; }
    public ApplicationUser? Author { get; set; }
    
    public ICollection<TestTaskResult>? Results { get; set; }
}