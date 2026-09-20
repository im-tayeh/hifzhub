using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class StaffProfileConfiguration : IEntityTypeConfiguration<StaffProfile>
{
    public void Configure(EntityTypeBuilder<StaffProfile> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.CenterId);
        builder.HasIndex(p => p.UserId).IsUnique();
        builder.Property(p => p.Bio).HasMaxLength(1000);
        builder.Property(p => p.Qualifications).HasMaxLength(1000);
        builder.Property(p => p.Certificates).HasMaxLength(1000);

        builder.HasOne(p => p.Center).WithMany().HasForeignKey(p => p.CenterId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}