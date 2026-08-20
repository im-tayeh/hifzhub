using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class HalaqaConfiguration : IEntityTypeConfiguration<Halaqa>
{
    public void Configure(EntityTypeBuilder<Halaqa> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Name).IsRequired().HasMaxLength(150);
        builder.HasIndex(h => h.CenterId);

        builder.HasOne(h => h.Center)
               .WithMany()
               .HasForeignKey(h => h.CenterId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.Stage)
               .WithMany()
               .HasForeignKey(h => h.StageId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}