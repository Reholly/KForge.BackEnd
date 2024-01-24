using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.User;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.HasKey(au => au.Id);
        builder.HasIndex(au => au.Username).IsUnique();

        builder.HasMany(au => au.CoursesAsMentor)
            .WithMany(c => c.Mentors);

        builder.HasMany(au => au.CoursesAsStudent)
            .WithMany(c => c.Students);

        builder.HasMany(au => au.TasksAsAuthor)
            .WithOne(tt => tt.Author)
            .HasForeignKey(tt => tt.AuthorId);

        builder.HasMany(au => au.TestTaskResults)
            .WithOne(ttr => ttr.Student)
            .HasForeignKey(ttr => ttr.StudentId);

        builder.HasMany(x => x.Groups)
            .WithMany(x => x.Users);
    }
}