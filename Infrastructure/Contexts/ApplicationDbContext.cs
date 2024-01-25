using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Group> Groups => Set<Group>();
    
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Tag> Tags => Set<Tag>();
    
    public DbSet<TestTask> TestTasks => Set<TestTask>();
    public DbSet<TestTaskResult> Results => Set<TestTaskResult>();
    public DbSet<Lecture> Lectures => Set<Lecture>();
    
    public DbSet<ApplicationUser> Profiles => Set<ApplicationUser>();
    
    public ApplicationDbContext() { }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}