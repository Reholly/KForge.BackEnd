using Domain.Entities.Base;

namespace Domain.Entities;

public class TestTask : EntityBase
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public ICollection<Question>? Questions { get; set; }
    public required string AuthorEmail { get; set; }
    public ApplicationUser? Author { get; set; }
    public ICollection<TestTaskResult>? Results { get; set; }
}