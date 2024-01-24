using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Administration;

public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Courses)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId);
        
        builder.HasOne(x => x.Department)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Groups);
    }
}