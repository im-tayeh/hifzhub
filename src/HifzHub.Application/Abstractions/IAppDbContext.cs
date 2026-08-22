using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<Center> Centers { get; }
    DbSet<User> Users { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<Stage> Stages { get; }
    DbSet<Halaqa> Halaqat { get; }
    DbSet<Student> Students { get; }
    DbSet<TrainingProgram> Programs { get; }
    DbSet<Course> Courses { get; }
    DbSet<CourseEnrollment> CourseEnrollments { get; }
    DbSet<Surah> Surahs { get; }
    DbSet<Attendance> Attendances { get; }
    DbSet<Recitation> Recitations { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}