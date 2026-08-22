using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class RecitationConfiguration : IEntityTypeConfiguration<Recitation>
{
    public void Configure(EntityTypeBuilder<Recitation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(r => r.CenterId);

        builder.HasIndex(r => new { r.StudentId, r.Date });

        builder.Property(r => r.Note).HasMaxLength(500);

        builder.HasOne(r => r.Center).WithMany().HasForeignKey(r => r.CenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Student).WithMany().HasForeignKey(r => r.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Halaqa).WithMany().HasForeignKey(r => r.HalaqaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}