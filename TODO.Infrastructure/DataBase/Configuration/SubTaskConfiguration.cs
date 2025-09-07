using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TODO.Core.Entity;

namespace TODO.Infrastructure.DataBase.Configuration;

public class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
{
    public void Configure(EntityTypeBuilder<SubTask> builder)
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

        builder.Property(s => s.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(s => s.IsCompleted)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasOne<ProjectTask>()
               .WithMany(s => s.SubTasks)
               .HasForeignKey(s => s.ProjectTask_Id)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
