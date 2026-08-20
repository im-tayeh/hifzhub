using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
{
    public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.CenterId);

        builder.HasIndex(e => new { e.CourseId, e.StudentId }).IsUnique();

        builder.HasOne(e => e.Course).WithMany(c => c.Enrollments)
               .HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.Student).WithMany()
               .HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);
    }
}