using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.CenterId);
        builder.Property(a => a.Note).HasMaxLength(500);

        builder.HasIndex(a => new { a.StudentId, a.Date }).IsUnique();

        builder.HasOne(a => a.Center).WithMany().HasForeignKey(a => a.CenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Student).WithMany().HasForeignKey(a => a.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Halaqa).WithMany().HasForeignKey(a => a.HalaqaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}