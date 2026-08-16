using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options,
    ITenantContext tenant) : DbContext(options), IAppDbContext
{
    public DbSet<Center> Centers => Set<Center>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Stage> Stages => Set<Stage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<User>().HasQueryFilter(u =>
            tenant.CenterId == null || u.CenterId == tenant.CenterId);

        modelBuilder.Entity<Stage>().HasQueryFilter(s =>
            tenant.CenterId == null || s.CenterId == tenant.CenterId);
    }
}