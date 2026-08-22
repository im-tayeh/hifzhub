using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class SurahConfiguration : IEntityTypeConfiguration<Surah>
{
    public void Configure(EntityTypeBuilder<Surah> builder)
    {
        builder.HasKey(s => s.Number);

        builder.Property(s => s.Number)
            .ValueGeneratedNever();

        builder.Property(s => s.NameAr)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.NameEn)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.NameEn)
            .IsRequired()
            .HasMaxLength(50);
    }
}