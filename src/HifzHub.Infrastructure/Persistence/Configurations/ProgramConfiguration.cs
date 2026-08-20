using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class ProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
{
    public void Configure(EntityTypeBuilder<TrainingProgram> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(p => p.CenterId);
        builder.HasOne(p => p.Center).WithMany().HasForeignKey(p => p.CenterId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}