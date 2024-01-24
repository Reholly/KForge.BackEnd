using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class ApplicationUser : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    
    public ICollection<Group> Groups { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public ICollection<Course> CoursesAsStudent { get; set; } = new List<Course>();
    public ICollection<Course> CoursesAsMentor { get; set; } = new List<Course>();
    
    public ICollection<TestTaskResult>? TestTaskResults { get; set; }
    public ICollection<TestTask>? TasksAsAuthor { get; set; }
}