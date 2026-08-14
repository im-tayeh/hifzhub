using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HifzHub.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.FullName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.HasIndex(u => u.Username)
               .IsUnique();

        builder.HasMany(u => u.Roles)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}