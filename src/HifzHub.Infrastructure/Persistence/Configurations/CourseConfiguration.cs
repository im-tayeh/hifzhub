using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(c => c.CenterId);
        builder.HasOne(c => c.Center).WithMany().HasForeignKey(c => c.CenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Program).WithMany().HasForeignKey(c => c.ProgramId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}