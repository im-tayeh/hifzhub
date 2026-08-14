using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using HifzHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync (AppDbContext context, IPasswordHasher hasher)
    {
        if (await context.Users.AnyAsync()) return;

        var admin = new User
        {
            CenterId = null,
            Username = "imtayeh",
            FullName = "IMTAYEH",
            PasswordHash = hasher.Hash("4720"),
            IsActive = true,
        };

        admin.Roles.Add(new UserRole
        {
            Role = RoleType.SuperAdmin,
            ScopeType = ScopeType.Center
        });

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}