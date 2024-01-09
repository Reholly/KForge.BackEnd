namespace Domain.Entities;

public class ApplicationUser
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public ICollection<Course> CoursesAsStudent { get; set; } = new List<Course>();
    public ICollection<Course> CoursesAsMentor { get; set; } = new List<Course>();
}