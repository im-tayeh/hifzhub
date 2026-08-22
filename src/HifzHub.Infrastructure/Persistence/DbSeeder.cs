using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using HifzHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync (AppDbContext db, IPasswordHasher hasher)
    {
        if (!await db.Surahs.AnyAsync())
        {
            db.Surahs.AddRange(SurahData.Surahs);
            await db.SaveChangesAsync();
        }

        if (!await db.Users.AnyAsync(u => u.Username == "imtayeh"))
        {
            var superAdmin = new User
            {
                CenterId = null,
                Username = "imtayeh",
                FullName = "IMTAYEH",
                PasswordHash = hasher.Hash("4720"),
                IsActive = true
            };
            superAdmin.Roles.Add(new UserRole
            {
                Role = RoleType.SuperAdmin,
                ScopeType = ScopeType.Center
            });
            db.Users.Add(superAdmin);
        }

        if (!await db.Centers.AnyAsync(c => c.Code == "demo"))
        {
            var demoCenter = new Center
            {
                Name = "Demo Center",
                Code = "demo",
                Status = CenterStatus.Active,
                IsDemo = true
            };
            db.Centers.Add(demoCenter);

            var centerAdmin = new User
            {
                CenterId = demoCenter.Id,
                Username = "admin",
                FullName = "Center Admin",
                PasswordHash = hasher.Hash("Admin@123"),
                IsActive = true
            };
            centerAdmin.Roles.Add(new UserRole
            {
                CenterId = demoCenter.Id,
                Role = RoleType.CenterAdmin,
                ScopeType = ScopeType.Center
            });
            db.Users.Add(centerAdmin);
        }

        await db.SaveChangesAsync();
    }
}