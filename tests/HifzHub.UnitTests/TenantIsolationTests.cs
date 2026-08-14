using FluentAssertions;
using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using HifzHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HifzHub.UnitTests;

public class FakeTenantContext : ITenantContext
{
    public Guid? CenterId { get; set; }
}

public class TenantIsolationTests
{
    private static AppDbContext BuildContext(string dbName, Guid? currentCenter)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        var tenant = new FakeTenantContext { CenterId = currentCenter };
        return new AppDbContext(options, tenant);
    }

    [Fact]
    public async Task Center_A_cannot_see_users_of_center_B()
    {
        var centerA = Guid.NewGuid();
        var centerB = Guid.NewGuid();
        var dbName = Guid.NewGuid().ToString();

        await using (var seed = BuildContext(dbName, null))
        {
            seed.Users.Add(new User { CenterId = centerA, Username = "ahmed", FullName = "Ahmed", PasswordHash = "x" });
            seed.Users.Add(new User { CenterId = centerB, Username = "salem", FullName = "Salem", PasswordHash = "x" });
            await seed.SaveChangesAsync();
        }

        await using var ctx = BuildContext(dbName, centerA);
        var visible = await ctx.Users.ToListAsync();

        visible.Should().HaveCount(1);
        visible.Should().OnlyContain(u => u.CenterId == centerA);
        visible.Should().NotContain(u => u.Username == "salem");
    }

    [Fact]
    public async Task SuperAdmin_can_see_all_centers()
    {
        var centerA = Guid.NewGuid();
        var centerB = Guid.NewGuid();
        var dbName = Guid.NewGuid().ToString();

        await using (var seed = BuildContext(dbName, null))
        {
            seed.Users.Add(new User { CenterId = centerA, Username = "ahmed", FullName = "Ahmed", PasswordHash = "x" });
            seed.Users.Add(new User { CenterId = centerB, Username = "salem", FullName = "Salem", PasswordHash = "x" });
            await seed.SaveChangesAsync();
        }

        await using var ctx = BuildContext(dbName, null);
        var visible = await ctx.Users.ToListAsync();

        visible.Should().HaveCount(2);
    }
}