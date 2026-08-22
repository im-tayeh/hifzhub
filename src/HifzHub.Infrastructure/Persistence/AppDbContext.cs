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
    public DbSet<Halaqa> Halaqat => Set<Halaqa>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<TrainingProgram> Programs => Set<TrainingProgram>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseEnrollment> CourseEnrollments => Set<CourseEnrollment>();
    public DbSet<Surah> Surahs => Set<Surah>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Recitation> Recitations => Set<Recitation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<User>().HasQueryFilter(u =>
            tenant.CenterId == null || u.CenterId == tenant.CenterId);

        modelBuilder.Entity<Stage>().HasQueryFilter(s =>
            tenant.CenterId == null || s.CenterId == tenant.CenterId);

        modelBuilder.Entity<Halaqa>().HasQueryFilter(h =>
            tenant.CenterId == null || h.CenterId == tenant.CenterId);

        modelBuilder.Entity<Student>().HasQueryFilter(s =>
            tenant.CenterId == null || s.CenterId == tenant.CenterId);

        modelBuilder.Entity<TrainingProgram>().HasQueryFilter(p =>
            tenant.CenterId == null || p.CenterId == tenant.CenterId);

        modelBuilder.Entity<Course>().HasQueryFilter(c =>
            tenant.CenterId == null || c.CenterId == tenant.CenterId);

        modelBuilder.Entity<CourseEnrollment>().HasQueryFilter(e =>
            tenant.CenterId == null || e.CenterId == tenant.CenterId);

        modelBuilder.Entity<Attendance>().HasQueryFilter(a =>
            tenant.CenterId == null || a.CenterId == tenant.CenterId);

        modelBuilder.Entity<Recitation>().HasQueryFilter(r =>
            tenant.CenterId == null || r.CenterId == tenant.CenterId);
    }
}