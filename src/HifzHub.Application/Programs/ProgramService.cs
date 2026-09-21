using HifzHub.Application.Abstractions;
using HifzHub.Domain.Common;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Programs;

public class ProgramService(IAppDbContext db, ITenantContext tenant)
{
    // ===== Programs =====
    public async Task<Result<ProgramResponse>> CreateProgramAsync(CreateProgramRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null)
            return Result<ProgramResponse>.Validation("A center context is required.");

        var p = new TrainingProgram { CenterId = tenant.CenterId.Value, Name = req.Name, Description = req.Description };
        db.Programs.Add(p);
        await db.SaveChangesAsync(ct);
        return Result<ProgramResponse>.Success(new ProgramResponse(p.Id, p.Name, p.Description));
    }

    public async Task<List<ProgramResponse>> GetProgramsAsync(CancellationToken ct = default) =>
        await db.Programs.Select(p => new ProgramResponse(p.Id, p.Name, p.Description)).ToListAsync(ct);

    // ===== Courses =====
    public async Task<Result<CourseResponse>> CreateCourseAsync(CreateCourseRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null)
            return Result<CourseResponse>.Validation("A center context is required.");
        if (req.ProgramId is not null && !await db.Programs.AnyAsync(p => p.Id == req.ProgramId, ct))
            return Result<CourseResponse>.NotFound("Program not found.");

        var c = new Course { CenterId = tenant.CenterId.Value, Name = req.Name, ProgramId = req.ProgramId };
        db.Courses.Add(c);
        await db.SaveChangesAsync(ct);
        return Result<CourseResponse>.Success(new CourseResponse(c.Id, c.Name, c.ProgramId));
    }

    public async Task<List<CourseResponse>> GetCoursesAsync(CancellationToken ct = default) =>
        await db.Courses.Select(c => new CourseResponse(c.Id, c.Name, c.ProgramId)).ToListAsync(ct);

    // ===== Enrollment =====
    public async Task<Result<EnrollmentResponse>> EnrollAsync(Guid courseId, Guid studentId, CancellationToken ct = default)
    {
        if (tenant.CenterId is null)
            return Result<EnrollmentResponse>.Validation("A center context is required.");
        if (!await db.Courses.AnyAsync(c => c.Id == courseId, ct))
            return Result<EnrollmentResponse>.NotFound("Course not found.");
        if (!await db.Students.AnyAsync(s => s.Id == studentId, ct))
            return Result<EnrollmentResponse>.NotFound("Student not found.");
        if (await db.CourseEnrollments.AnyAsync(e => e.CourseId == courseId && e.StudentId == studentId, ct))
            return Result<EnrollmentResponse>.Conflict("Student is already enrolled in this course.");

        var e = new CourseEnrollment { CenterId = tenant.CenterId.Value, CourseId = courseId, StudentId = studentId };
        db.CourseEnrollments.Add(e);
        await db.SaveChangesAsync(ct);
        return Result<EnrollmentResponse>.Success(new EnrollmentResponse(e.Id, e.CourseId, e.StudentId));
    }

    public async Task<List<Guid>> GetCourseStudentsAsync(Guid courseId, CancellationToken ct = default) =>
        await db.CourseEnrollments.Where(e => e.CourseId == courseId).Select(e => e.StudentId).ToListAsync(ct);
}