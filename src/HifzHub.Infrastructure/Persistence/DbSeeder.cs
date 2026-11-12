using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using HifzHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HifzHub.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, IPasswordHasher hasher, IConfiguration config)
    {
        if (!await db.Surahs.AnyAsync())
        {
            db.Surahs.AddRange(SurahData.Surahs);
            await db.SaveChangesAsync();
        }

        // Super admin credentials come from configuration only (user-secrets / env vars).
        // No fallback: if they are missing, the super admin is simply not created.
        var superUsername = config["Seed:SuperAdmin:Username"];
        var superPassword = config["Seed:SuperAdmin:Password"];

        if (!string.IsNullOrWhiteSpace(superUsername) &&
            !string.IsNullOrWhiteSpace(superPassword) &&
            !await db.Users.IgnoreQueryFilters().AnyAsync(u => u.Username == superUsername))
        {
            var superAdmin = new User
            {
                CenterId = null,
                Username = superUsername,
                FullName = "Super Admin",
                PasswordHash = hasher.Hash(superPassword),
                IsActive = true
            };
            superAdmin.Roles.Add(new UserRole { Role = RoleType.SuperAdmin, ScopeType = ScopeType.Center });
            db.Users.Add(superAdmin);
        }

        // Demo tenant: public credentials on purpose, so anyone can try the demo
        if (!await db.Centers.IgnoreQueryFilters().AnyAsync(c => c.Code == "demo"))
        {
            var demoCenter = new Center { Name = "Demo Center", Code = "demo", Status = CenterStatus.Active, IsDemo = true };
            db.Centers.Add(demoCenter);

            var demoAdmin = new User
            {
                CenterId = demoCenter.Id,
                Username = "admin",
                FullName = "Demo Center Admin",
                PasswordHash = hasher.Hash("Admin@123"),
                IsActive = true
            };
            demoAdmin.Roles.Add(new UserRole { CenterId = demoCenter.Id, Role = RoleType.CenterAdmin, ScopeType = ScopeType.Center });
            db.Users.Add(demoAdmin);
        }

        await db.SaveChangesAsync();
    }
}