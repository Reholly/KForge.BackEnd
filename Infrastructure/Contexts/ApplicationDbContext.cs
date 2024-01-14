using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext
{
    public DbSet<ApplicationUser> Profiles => Set<ApplicationUser>();
    public DbSet<TestTask> TestTasks => Set<TestTask>();
    public DbSet<Course> Courses => Set<Course>();
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