using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.FullName).IsRequired().HasMaxLength(200);
        builder.Property(s => s.NationalId).HasMaxLength(50);
        builder.Property(s => s.WhatsApp).HasMaxLength(20);
        builder.HasIndex(s => s.CenterId);

        builder.HasOne(s => s.Center)
               .WithMany()
               .HasForeignKey(s => s.CenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Halaqa)
               .WithMany()
               .HasForeignKey(s => s.HalaqaId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}