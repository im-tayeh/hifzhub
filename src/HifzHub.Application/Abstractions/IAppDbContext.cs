using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<Center> Centers { get; }
    DbSet<User> Users { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<Stage> Stages { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}