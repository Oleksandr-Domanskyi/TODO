using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TODO.Core.Entity;

namespace TODO.Infrastructure.DataBase.Configuration;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .HasMaxLength(1000);

        builder.Property(s => s.CreatedDate)
            .IsRequired();

        builder.Property(s => s.ExpiryDate)
            .IsRequired();

        builder.HasMany(s => s.SubTasks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(s => s.ExpiryDate);
    }
}
