using HifzHub.Application.Abstractions;
using HifzHub.Domain.Common;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HifzHub.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options,
    ITenantContext tenant) : DbContext(options), IAppDbContext
{
    private readonly ITenantContext _tenant = tenant;

    public DbSet<Center> Centers => Set<Center>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Stage> Stages => Set<Stage>();
    public DbSet<Halaqa> Halaqat => Set<Halaqa>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<TrainingProgram> Programs => Set<TrainingProgram>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseEnrollment> CourseEnrollments => Set<CourseEnrollment>();
    public DbSet<Surah> Surahs => Set<Surah>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Recitation> Recitations => Set<Recitation>();
    public DbSet<StaffProfile> StaffProfiles => Set<StaffProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        modelBuilder.Entity<UserRole>().HasQueryFilter(r =>
            _tenant.CenterId == null || r.CenterId == _tenant.CenterId);

        // Automatic tenant isolation filter for every ITenantOwned entity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantOwned).IsAssignableFrom(entityType.ClrType))
            {
                var param = Expression.Parameter(entityType.ClrType, "e");
                var centerId = Expression.Property(param, nameof(ITenantOwned.CenterId));
                var current = Expression.Property(Expression.Constant(this), nameof(CurrentCenterId));

                // e => CurrentCenterId == null || e.CenterId == CurrentCenterId
                var body = Expression.OrElse(
                    Expression.Equal(current, Expression.Constant(null, typeof(Guid?))),
                    Expression.Equal(Expression.Convert(centerId, typeof(Guid?)), current));

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, param));
            }
        }

        // Entities with a nullable CenterId (super admin has no center) cannot implement
        // ITenantOwned, so their filters are declared explicitly here.
        modelBuilder.Entity<User>().HasQueryFilter(u =>
            CurrentCenterId == null || u.CenterId == CurrentCenterId);

        modelBuilder.Entity<UserRole>().HasQueryFilter(r =>
            CurrentCenterId == null || r.CenterId == CurrentCenterId);
    }

    // Exposes the current tenant to the query-filter expressions above
    private Guid? CurrentCenterId => _tenant.CenterId;
}