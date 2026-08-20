using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;

namespace HifzHub.Application.Programs;
using Microsoft.EntityFrameworkCore;


public class ProgramService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<ProgramResponse?> CreateProgramAsync(CreateProgramRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;
        var p = new TrainingProgram { CenterId = tenant.CenterId.Value, Name = req.Name, Description = req.Description };
        db.Programs.Add(p);
        await db.SaveChangesAsync(ct);
        return new ProgramResponse(p.Id, p.Name, p.Description);
    }

    public async Task<List<ProgramResponse>> GetProgramsAsync(CancellationToken ct = default) =>
        await db.Programs.Select(p => new ProgramResponse(p.Id, p.Name, p.Description)).ToListAsync(ct);

    public async Task<CourseResponse?> CreateCourseAsync(CreateCourseRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;
        if (req.ProgramId is not null && !await db.Programs.AnyAsync(p => p.Id == req.ProgramId, ct))
            return null;

        var c = new Course { CenterId = tenant.CenterId.Value, Name = req.Name, ProgramId = req.ProgramId };
        db.Courses.Add(c);
        await db.SaveChangesAsync(ct);
        return new CourseResponse(c.Id, c.Name, c.ProgramId);
    }

    public async Task<List<CourseResponse>> GetCoursesAsync(CancellationToken ct = default) =>
        await db.Courses.Select(c => new CourseResponse(c.Id, c.Name, c.ProgramId)).ToListAsync(ct);

    public async Task<EnrollmentResponse?> EnrollAsync(Guid courseId, Guid studentId, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;
        if (!await db.Courses.AnyAsync(c => c.Id == courseId, ct)) return null;
        if (!await db.Students.AnyAsync(s => s.Id == studentId, ct)) return null;
        if (await db.CourseEnrollments.AnyAsync(e => e.CourseId == courseId && e.StudentId == studentId, ct))
            return null;

        var e = new CourseEnrollment { CenterId = tenant.CenterId.Value, CourseId = courseId, StudentId = studentId };
        db.CourseEnrollments.Add(e);
        await db.SaveChangesAsync(ct);
        return new EnrollmentResponse(e.Id, e.CourseId, e.StudentId);
    }

    public async Task<List<Guid>> GetCourseStudentsAsync(Guid courseId, CancellationToken ct = default) =>
        await db.CourseEnrollments.Where(e => e.CourseId == courseId)
                                  .Select(e => e.StudentId).ToListAsync(ct);
}